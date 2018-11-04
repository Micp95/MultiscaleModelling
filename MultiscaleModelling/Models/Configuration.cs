using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Models
{
    public class Configuration
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfGrains { get; set; }
        public int NumberOfSubGrains { get; set; }
        public BCEnum BoundaryConditions { get; set; } 
        public NeighbourhoodEnum Neighbourhood { get; set; }
        public int MooreProbability { get; set; }
        public StructureTypeEnume StructureTypeEnume { get; set; }
    }
}
