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

        public Bitmap GetBitmap()
        {
            return _mapController.GetBitmap();
        }

        public void Initialize(Configuration config)
        {
            _grains = new List<Grain>();
            _random = new Random();
            _configuration = config;
            _mapController = new MapController(config.Width, config.Height);

            int grainsToGenerate = config.NumberOfGrains;
            while(grainsToGenerate > 0)
            {
                var grain = GetRandomGrain();
                AddGrainToRandomPosition(grain);

                grainsToGenerate--;
            }

            _mapController.Commit();
        }

        public void NextStep()
        {
            for(int x = 1; x <= _configuration.Width; x++)
            {
                for (int y = 1; y <= _configuration.Height; y++)
                {
                    ProcessCoordinate(x, y);
                }
            }
            _mapController.Commit();
        }


        private void ProcessCoordinate(int x, int y)
        {
            var node = _mapController.GetNode(x, y);

            if (node.Type == TypeEnum.Empty)
            {
                var neighbourhood = _mapController.GetNeighbourhoods(x, y, _configuration.Neighbourhood);

                neighbourhood = neighbourhood.Where(k => k.Type == TypeEnum.Grain).ToList();
                if (neighbourhood.Any())
                {
                    var orderedNeighbourhood = neighbourhood.GroupBy(s => s).Select(g => new KeyValuePair<Node,int>(g.First(),g.Count())).OrderByDescending(p=>p.Value).ToList();
                    var winner = orderedNeighbourhood.FirstOrDefault().Key;

                    node = winner;
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
            int x = _random.Next(1, _configuration.Width);
            int y = _random.Next(1, _configuration.Height);

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
            int id = _random.Next();
            Color color = Color.FromArgb(_random.Next(2,254), _random.Next(2, 254), _random.Next(2, 254));

            return new Grain()
            {
                Id = id,
                Color = color
            };
        }



    }
}
