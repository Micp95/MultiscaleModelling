using MultiscaleModelling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiscaleModelling.Interfaces;

namespace MultiscaleModelling.Core
{
    class RecrystallizationEngine : IProcessable
    {
        private SimulationEngine _simulationEngine;
        private Random _random;


        public RecrystallizationEngine(Random random, SimulationEngine simulationEngine)
        {
            _random = random;
            _simulationEngine = simulationEngine;
        }


        public void InitializeStep(int numberOfGrains)
        {
            throw new NotImplementedException();
        }

        public void NextStep()
        {
            throw new NotImplementedException();
        }
    }
}
