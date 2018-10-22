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

        public Bitmap GetBitmap()
        {
            return _mapController.GetBitmap();
        }

        public StandardSimulation()
        {
            _grains = new List<Grain>();
            _random = new Random();
            _isMapEmpty = true;
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
        }

        public void Restart()
        {
            _mapController = new MapController(_configuration.Width, _configuration.Height);
            _mapController.Commit();
            _endSimulation = false;
            _isMapEmpty = true;
        }

        public bool IsMapEmpty()
        {
            return _isMapEmpty;
        }
        public void SeedGrains(Configuration config)
        {
            _configuration = config;
            _currentGrainId = 100;
            int grainsToGenerate = config.NumberOfGrains;
            while(grainsToGenerate > 0)
            {
                var grain = GetRandomGrain();
                AddGrainToRandomPosition(grain);

                grainsToGenerate--;
            }
            _mapController.Commit();
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
                        var node = new Node()
                        {
                            Type = TypeEnum.Inclusion,
                            X = x,
                            Y = y,
                            Id = seed.Id
                        };
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
                    var node = new Node()
                    {
                        Type = TypeEnum.Inclusion,
                        X = x,
                        Y = y,
                        Id = seed.Id
                    };
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

            for (int x = 1; x < _configuration.Width -1; x++)
            {
                for (int y = 1; y < _configuration.Height -1 ; y++)
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

                var neighbourhood = _mapController.GetNeighbourhoods(x, y, _configuration.Neighbourhood);

                neighbourhood = neighbourhood.Where(k => k.Type == TypeEnum.Grain).ToList();
                if (neighbourhood.Any())
                {
                    var orderedNeighbourhood = neighbourhood.GroupBy(s => s).Select(g => new KeyValuePair<Node,int>(g.First(),g.Count())).OrderByDescending(p=>p.Value).ToList();
                    var winnerValue = orderedNeighbourhood.FirstOrDefault().Value;

                    var winners = orderedNeighbourhood.Where(p=>p.Value== winnerValue).ToList();
                    var randomWinner = winners[_random.Next(winners.Count)].Key;

                    node = randomWinner;
                }
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
            Color color = Color.FromArgb(_random.Next(2,254), _random.Next(2, 254), _random.Next(2, 254));

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
            _configuration.Width = _mapController.Width-1;
            _configuration.Height = _mapController.Height-1;
        }

        public bool IsEndSimulation()
        {
            return _endSimulation;
        }
    }
}
