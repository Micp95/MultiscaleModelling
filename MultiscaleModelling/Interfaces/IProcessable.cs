using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Interfaces
{
    interface IProcessable
    {
        void NextStep();
        void InitializeStep(int numberOfGrains);
    }
}
