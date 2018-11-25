using MultiscaleModelling.Models;
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
        Bitmap GetBitmapGrainsSelection(bool visibility);
        void ExportToFile(FileTypeEnum type, string fileName);
        void ImportFromFile(FileTypeEnum type, string fileName);
        void AddInclusions(ConfigurationInclusions config);
        void SeedGrains(int numberOfGrains);
        Configuration GetConfiguration();
        bool IsMapEmpty();

        bool IsEndSimulation();

        void AddOrRemoveGrainsToSelectLis(int x, int y);
        void RestartSelectedList();
        void StartGenerateSubstructure(Configuration config);
        void AddBoundariesForGrains(Configuration config);
        void RemoveGrainsColors();
        float GetGBPercent();
        void FirstStepMC();
    }
}
