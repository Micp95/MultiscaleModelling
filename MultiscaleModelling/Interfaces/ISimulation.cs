﻿using MultiscaleModelling.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Interfaces
{
    public interface ISimulation
    {
        void Initialize(Configuration config);
        void NextStep();
        Bitmap GetBitmap();

    }
}
