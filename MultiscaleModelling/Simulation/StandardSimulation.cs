using MultiscaleModelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MultiscaleModelling.Controllers;
using MultiscaleModelling.Models;

namespace MultiscaleModelling.Simulation
{
    class StandardSimulation : ISimulation
    {
        MapController _mapController;
        Configuration _configuration;
        Random _random;
        List<Grain> _grains;
        private bool _endSimulation;
        private bool _isMapEmpty;
        private int _currentGrainId;
        List<Grain> _selectedGrains;

        public Bitmap GetBitmap()
        {
            return _mapController.GetBitmap();
        }

        public StandardSimulation()
        {
            _grains = new List<Grain>();
            _selectedGrains = new List<Grain>();
            _random = new Random();
            _isMapEmpty = true;
            _currentGrainId = 100;
        }
        public void Initialize(Configuration config)
        {
            if(_configuration == null || _configuration.Width != config.Width || _configuration.Width != config.Height)
            {
                _mapController = new MapController(config.Width, config.Height);
                _mapController.Commit();
            }else
            {
                _mapController.CopyMap();
            }
            _configuration = config;
            _isMapEmpty = false;
            _selectedGrains.Clear();
        }

        public void Restart()
        {
            _mapController = new MapController(_configuration.Width, _configuration.Height);
            _mapController.Commit();
            _endSimulation = false;
            _isMapEmpty = true;
            _selectedGrains.Clear();
        }

        public bool IsMapEmpty()
        {
            return _isMapEmpty;
        }
        public void SeedGrains(int numberOfGrains)
        {
            int grainsToGenerate = numberOfGrains;
            while(grainsToGenerate > 0)
            {
                var grain = GetRandomGrain();
                _grains.Add(grain);
                AddGrainToRandomPosition(grain);

                _mapController.Commit();
                _mapController.CopyMap();
                grainsToGenerate--;
            }

            GenerateListOfGrains();
        }



        public void AddInclusions(ConfigurationInclusions config)
        {
            int inclusionsToAdd = config.NumberOfInclusions;
            while(inclusionsToAdd > 0)
            {
                _mapController.CopyMap();
                AddInclusion(config.InclusionType, config.SizeOfInclusions);
                inclusionsToAdd--;
                _mapController.Commit();
            }
            _isMapEmpty = false;
        }
        public void NextStep()
        {
            _endSimulation = true;
            for (int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    ProcessCoordinate(x, y);
                }
            }
            _mapController.Commit();

            if (_endSimulation)
                EndSubstructureSimulation();
        }


        private void AddInclusion(InclusionType type, int size)
        {
            var seed = GetSeedForInclusion();

            if(type == InclusionType.Circle)
                GrowCircleInclusion(seed, size);
            else if ( type == InclusionType.Square)
                GrowSquareInclusion(seed, size);
        }

        private void GrowCircleInclusion (Node seed, int size)
        {
            double squareSize = Math.Pow((double)size/2.0,2);
            for (int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    double distance = (Math.Pow(x - seed.X, 2) + Math.Pow(y - seed.Y, 2));
                    if(distance < squareSize)
                    {
                        var node = _mapController.GetInclusionNode(x, y);
                        _mapController.SetNode(x, y, node);
                    }

                }
            }
        }
        private void GrowSquareInclusion(Node seed, int size)
        {

            int halfSize = size / 2;

            for(int x = seed.X - halfSize; x < seed.X - halfSize + size; x++)
            {
                for (int y = seed.Y - halfSize; y < seed.Y - halfSize + size; y++)
                {
                    var node = _mapController.GetInclusionNode(x, y);
                    _mapController.SetNode(x, y, node);
                }
            }

        }
        private Node GetSeedForInclusion ()
        {
            if (IsEndSimulation())
                return GetSeedForInclusionAfter();
            else
                return GetSeedForInclusionBefore();
        }

        private Node GetSeedForInclusionAfter()
        {
            var edgesdNodes = GetListOfEdgesNodes();

            int rand = _random.Next(edgesdNodes.Count);

            return edgesdNodes[rand];
        }

        private Node GetSeedForInclusionBefore()
        {
            int x, y;
            bool correctCoordinate = false;
            do
            {
                x = _random.Next(1, _configuration.Width);
                y = _random.Next(1, _configuration.Height);

                correctCoordinate = _mapController.GetNode(x, y).Type == TypeEnum.Empty;
            } while (!correctCoordinate);


            var node = new Node()
            {
                X = x,
                Y = y,
                Id = 3,
                Type = TypeEnum.Inclusion
            };

            return node;
        }

        private List<Node> GetListOfEdgesNodes()
        {
            List<Node> edgesNodes = new List<Node>();

            for (int x = 1; x < _configuration.Width; x++)
            {
                for (int y = 1; y < _configuration.Height ; y++)
                {
                    var myNode = _mapController.GetNode(x, y);
                    var neighbourhood = _mapController.GetNeighbourhoods(x, y, NeighbourhoodEnum.Moore);
                    int otherGrainCount = neighbourhood.Count(k => k.Type == TypeEnum.Grain && k.Id != myNode.Id);

                    if (otherGrainCount != 0)
                        edgesNodes.Add(myNode);
                }
            }

            return edgesNodes;
        }
        private void ProcessCoordinate(int x, int y)
        {
            var node = _mapController.GetNode(x, y);

            if (node.Type == TypeEnum.Empty)
            {
                _endSimulation = false;

                node = GetNodeForCA(node, _configuration.Neighbourhood);
            }


            _mapController.SetNode(x, y, new Node()
            {
                X = x,
                Y = y,
                Id = node.Id,
                Color = node.Color,
                Type = node.Type
            });
        }

        private Node GetNodeForCA(Node node, NeighbourhoodEnum type)
        {
            switch (type)
            {
                case NeighbourhoodEnum.Moore:
                case NeighbourhoodEnum.VonNeumann:
                    return GetNodeforStandardMethod(node);
                case NeighbourhoodEnum.Moore2:
                    return GetNodeforModificationMethod(node);
            }
            return null;
        }
        
        private Node GetNodeforStandardMethod(Node node)
        {
            var neighbourhood = _mapController.GetNeighbourhoods(node.X, node.Y, _configuration.Neighbourhood);

            neighbourhood = neighbourhood.Where(k => k.Type == TypeEnum.Grain).ToList();
            if (neighbourhood.Any())
            {
                var orderedNeighbourhood = neighbourhood.GroupBy(s => s).Select(g => new KeyValuePair<Node, int>(g.First(), g.Count())).OrderByDescending(p => p.Value).ToList();
                var winnerValue = orderedNeighbourhood.FirstOrDefault().Value;

                var winners = orderedNeighbourhood.Where(p => p.Value == winnerValue).ToList();
                var randomWinner = winners[_random.Next(winners.Count)].Key;

                node = randomWinner;
            }

            return node;
        }
        private Node GetNodeforModificationMethod(Node node)
        {
            var mooreNeighbourhood = GetListOfGrainNeighbourhood(node, NeighbourhoodEnum.Moore);
            if (mooreNeighbourhood == null || !mooreNeighbourhood.Any())
                return node;
            if(mooreNeighbourhood.FirstOrDefault().Value >= 5)
            {
                return mooreNeighbourhood.FirstOrDefault().Key;
            }

            var secondNeighbourhood = GetListOfGrainNeighbourhood(node, NeighbourhoodEnum.VonNeumann);
            if (secondNeighbourhood != null && secondNeighbourhood.FirstOrDefault().Value >= 3)
            {
                return secondNeighbourhood.FirstOrDefault().Key;
            }

            var trNeighbourhood = GetListOfGrainNeighbourhood(node, NeighbourhoodEnum.Cross);
            if (trNeighbourhood != null && trNeighbourhood.FirstOrDefault().Value >= 3)
            {
                return trNeighbourhood.FirstOrDefault().Key;
            }

            int randomNumber = _random.Next(100);
            if(randomNumber < _configuration.MooreProbability)
            {
                var winnerValue = mooreNeighbourhood.FirstOrDefault().Value;
                var winners = mooreNeighbourhood.Where(p => p.Value == winnerValue).ToList();
                return winners[_random.Next(winners.Count)].Key;
            }

            return node;
        }

        private List<KeyValuePair<Node,int>> GetListOfGrainNeighbourhood(Node node, NeighbourhoodEnum type)
        {
            var neighbourhood = _mapController.GetNeighbourhoods(node.X, node.Y, type);

            neighbourhood = neighbourhood.Where(k => k.Type == TypeEnum.Grain).ToList();

            if (neighbourhood.Any())
            {
                var orderedNeighbourhood = neighbourhood.GroupBy(s => s).Select(g => new KeyValuePair<Node, int>(g.First(), g.Count())).OrderByDescending(p => p.Value).ToList();
                return orderedNeighbourhood;
            }
            return null;
        }

        private void AddGrainToRandomPosition(Grain grain)
        {
            int x, y;
            bool correctCoordinate = false;
            do
            {
                x = _random.Next(1, _configuration.Width);
                y = _random.Next(1, _configuration.Height);

                correctCoordinate = _mapController.GetNode(x, y).Type == TypeEnum.Empty;
            } while (!correctCoordinate);

            var node = new Node()
            {
                X = x,
                Y = y,
                Color = grain.Color,
                Id = grain.Id,
                Type = TypeEnum.Grain
            };

            _mapController.SetNode(x, y, node);
        }
        private Grain GetRandomGrain()
        {
            int id = _currentGrainId++;
            Color color;
            bool isNewColor = false;
            do
            {
                color = Color.FromArgb(_random.Next(2,254), _random.Next(2, 254), _random.Next(2, 254));
                isNewColor = !_grains.Any(k => k.Color.GetHashCode() == color.GetHashCode());
            } while (!isNewColor);


            return new Grain()
            {
                Id = id,
                Color = color
            };
        }

        public void ExportToFile(FileTypeEnum type, string fileName)
        {
            _mapController.ExportToFile(fileName, type);
        }

        public void ImportFromFile(FileTypeEnum type, string fileName)
        {
            if (_mapController == null)
                _mapController = new MapController();
            _mapController.ImportFromFile(fileName, type);

            if(_configuration == null)
            {
                _configuration = new Configuration()
                {
                    BoundaryConditions = BCEnum.NonPeriodical,
                    Neighbourhood = NeighbourhoodEnum.Moore
                };
            }

            _endSimulation = true;
            _isMapEmpty = false;
            _configuration.Width = _mapController.Width-2;
            _configuration.Height = _mapController.Height-2;

            GenerateListOfGrains();
            _configuration.NumberOfGrains = _grains.Count;
        }

        public bool IsEndSimulation()
        {
            return _endSimulation;
        }

        public void AddOrRemoveGrainsToSelectLis(int x, int y)
        {
            var node = _mapController.GetNode(x, y);
            if(node.Id > 100)
            {
                if(!_selectedGrains.Any(k => k.Id == node.Id))
                {
                    _selectedGrains.Add(new Grain()
                    {
                        Color = node.Color,
                        Id = node.Id
                    });
                }else{
                    var toRemove = _selectedGrains.FirstOrDefault(k => k.Id == node.Id);
                    _selectedGrains.Remove(toRemove);
                }
            }
        }

        public void StartGenerateSubstructure(Configuration config)
        {
            Grain newGrain = null;
            if(config.StructureTypeEnume == StructureTypeEnume.DualPhase)
            {
                newGrain = GetRandomGrain();
            }

            for (int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    var node =_mapController.GetNode(x, y);
                    if(_selectedGrains.Any(k=>k.Id == node.Id)){
                        if(newGrain == null)
                        {
                            node.Type = TypeEnum.OldGrain;
                            _mapController.SetNode(x, y, node);
                        }else
                        {
                            _mapController.SetNode(x, y, new Node()
                            {
                                Color= newGrain.Color,
                                Id=newGrain.Id,
                                Type=TypeEnum.OldGrain,
                                X=x,
                                Y=y
                            });
                        }
                    }else if (node.Type ==TypeEnum.Inclusion || node.Type == TypeEnum.GrainBorder)
                    {
                        _mapController.SetNode(x, y, node);
                    }
                    else {
                        _mapController.SetNode(x, y, _mapController.GetEmptyNode(x, y));
                    }
                }
            }
            _mapController.Commit();
            _endSimulation = false;
            _mapController.CopyMap();

            if (config.StructureTypeEnume == StructureTypeEnume.DualPhase)
            {
                _selectedGrains = new List<Grain>();
                _selectedGrains.Add(newGrain);
            }
        }

        private void EndSubstructureSimulation()
        {
            for (int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    var node = _mapController.GetNode(x, y);
                    if (node.Type == TypeEnum.OldGrain)
                        node.Type = TypeEnum.Grain;
                }
            }
        }

        public Configuration GetConfiguration()
        {
            return _configuration;
        }

        private void GenerateListOfGrains()
        {
            _grains = new List<Grain>();

            for (int x = 1; x < _configuration.Width; x++)
            {
                for (int y = 1; y < _configuration.Height; y++)
                {
                    var node = _mapController.GetNode(x, y);
                    if((node.Type == TypeEnum.Grain || node.Type == TypeEnum.OldGrain)  && !_grains.Any(k=>k.Id == node.Id))
                    {
                        _grains.Add(new Grain()
                        {
                            Id = node.Id,
                            Color = node.Color
                        });
                    }
                }
            }

            _currentGrainId = _grains.Max(k => k.Id);
            _currentGrainId++;
        }

        public void RestartSelectedList()
        {
            _selectedGrains = new List<Grain>();
        }

        public Bitmap GetBitmapGrainsSelection(bool visibility)
        {
            if (visibility)
                return GetBitmap();
            else
            {
                var colorList = new List<int>();
                foreach(var grain in _selectedGrains)
                {
                    colorList.Add(grain.Id);
                }

                return _mapController.GetBitmapWithHiddenColors(colorList);
            }

        }

        public void AddBoundariesForGrains(Configuration config)
        {
            var grainList = new List<Grain>();

            if (_selectedGrains.Any())
                grainList = _selectedGrains;
            else
                grainList = _grains;

            foreach (var grain in grainList)
            {
                AddBorderForGrain(grain, config.SizeOfGB);
            }
        }

        public void RemoveGrainsColors()
        {
            _mapController.CopyMap();
            for (int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    var node = _mapController.GetNode(x, y);
                    if (node.Type == TypeEnum.Grain)
                    {
                        var empty = _mapController.GetEmptyNode(x, y);
                        _mapController.SetNode(x, y, empty);
                    }
                }
            }
            _mapController.Commit();
        }

        private void AddBorderForGrain(Grain grain, int size)
        {
            List<Node> grainNodes = new List<Node>();
            List<Node> grainNodesNew;
            for (int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    var node = _mapController.GetNode(x, y);
                    if(node.Id == grain.Id)
                        grainNodes.Add(node);
                }
            }

            while (size > 0)
            {
                grainNodesNew = new List<Node>();
                _mapController.CopyMap();
                foreach (var node in grainNodes)
                {
                    var neighbourhood = _mapController.GetNeighbourhoods(node.X, node.Y, NeighbourhoodEnum.Moore);
                    int otherGrainCount = neighbourhood.Count(k => (k.Type == TypeEnum.Grain || k.Type == TypeEnum.GrainBorder || k.Type == TypeEnum.Border) && k.Id != node.Id);
                    if(otherGrainCount != 0)
                    {
                        Node neNode = new Node()
                        {
                            Type = TypeEnum.GrainBorder,
                            Color = _mapController.GetGrainBorderColor(),
                            X = node.X,
                            Y = node.Y,
                            Id = 6,
                        };
                        _mapController.SetNode(node.X, node.Y, neNode);
                    }else
                    {
                        grainNodesNew.Add(node);
                    }
                }
                grainNodes = grainNodesNew;
                _mapController.Commit();
                size--;
            }


        }

        public float GetGBPercent()
        {
            float GBNode = 0;
            float max = _configuration.Width * _configuration.Height;
            for (int x = 1; x < _configuration.Width; x++)
            {
                for (int y = 1; y < _configuration.Height; y++)
                {
                    var node = _mapController.GetNode(x, y);
                    if (node.Type == TypeEnum.GrainBorder)
                        GBNode++;
                }
            }
            return GBNode / max * 100;
            //labelGBPer
        }
    }
}
