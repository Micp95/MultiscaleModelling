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
        void Restart();
        Bitmap GetBitmap();

        void ExportToFile(FileTypeEnum type, string fileName);
        void ImportFromFile(FileTypeEnum type, string fileName);
        void AddInclusions(ConfigurationInclusions config);
        void SeedGrains(Configuration config);
        bool IsMapEmpty();

        bool IsEndSimulation();
    }
}
