using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Models
{
    public enum TypeEnum
    {
        Empty=0,
        Border = 1,
        Grain = 2,
        Inclusion = 3,
        OldGrain = 4,
        GrainBorder = 5,
        Recrystallization = 6
    }
}
