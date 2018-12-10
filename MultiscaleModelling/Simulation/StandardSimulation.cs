using MultiscaleModelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MultiscaleModelling.Controllers;
using MultiscaleModelling.Models;
using MultiscaleModelling.Core;

namespace MultiscaleModelling.Simulation
{
    class StandardSimulation : ISimulation
    {
        private Random _random;


        BitmapEngine _bitmapEngine;
        InclusionsEngine _inclusionsEngine;
        SimulationEngine _simulationEngine;
        CAEngine _CAEngine;
        MCEngine _MCEngine;
        FileEngine _fileEngine;
        RecrystallizationEngine _recrystallizationEngine;

        IProcessable _processEngine;

        public StandardSimulation()
        {
            _random = new Random();
            _simulationEngine = new SimulationEngine(_random);

            _bitmapEngine = new BitmapEngine(_simulationEngine);
            _inclusionsEngine = new InclusionsEngine(_random, _simulationEngine);
            _CAEngine = new CAEngine(_random, _simulationEngine);
            _MCEngine = new MCEngine(_random, _simulationEngine);
            _fileEngine = new FileEngine(_simulationEngine);
            _recrystallizationEngine = new RecrystallizationEngine(_random, _simulationEngine);

        }


        public Bitmap GetBitmap()
        {
            return _bitmapEngine.GetBitmap();
        }

        public void Initialize(Configuration config)
        {
            _simulationEngine.Initialize(config);
            SetProcessEngine();
        }

        public void Restart()
        {
            _simulationEngine.Restart();
        }

        public void InitializeStep(int numberOfGrains)
        {
            _processEngine.InitializeStep(numberOfGrains);
        }

        public bool IsMapEmpty()
        {
            return _simulationEngine.IsMapEmpty;
        }

        public void AddInclusions(ConfigurationInclusions config)
        {
            _inclusionsEngine.AddInclusions(config);
        }

        public void NextStep()
        {
            _processEngine.NextStep();
        }

        public void ExportToFile(FileTypeEnum type, string fileName)
        {
            _fileEngine.ExportToFile(type, fileName);
        }

        public void ImportFromFile(FileTypeEnum type, string fileName)
        {
            _fileEngine.ImportFromFile(type, fileName);
        }

        public bool IsEndSimulation()
        {
            return _simulationEngine.EndSimulation;
        }

        public void AddOrRemoveGrainsToSelectLis(int x, int y)
        {
            _simulationEngine.AddOrRemoveGrainsToSelectLis(x, y);
        }

        public void StartGenerateSubstructure(Configuration config)
        {
            _simulationEngine.StartGenerateSubstructure(config);
            SetProcessEngine();
        }

        public Configuration GetConfiguration()
        {
            return _simulationEngine.Configuration;
        }

        public void RestartSelectedList()
        {
            _simulationEngine.RestartSelectedList();
        }

        public Bitmap GetBitmapGrainsSelection(bool visibility)
        {
            return _bitmapEngine.GetBitmapGrainsSelection(visibility);
        }

        public void AddBoundariesForGrains(Configuration config)
        {
            _simulationEngine.AddBoundariesForGrains(config);
        }

        public void RemoveGrainsColors()
        {
            _simulationEngine.RemoveGrainsColors();
        }

        public float GetGBPercent()
        {
            return _simulationEngine.GetGBPercent();
        }

        public void CalculateEnergy()
        {
            _simulationEngine.CalculateEnergy();
        }

        public Bitmap GetEnergyBitmap()
        {
            return _bitmapEngine.GetEnergyBitmap();
        }


        private void SetProcessEngine()
        {
            if (_simulationEngine.Configuration.IsMC)
                _processEngine = _MCEngine;
            else
                _processEngine = _CAEngine;

        }


    }
}
