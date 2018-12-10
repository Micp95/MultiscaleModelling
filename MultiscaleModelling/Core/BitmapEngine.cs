using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Core
{
    class BitmapEngine
    {

        private SimulationEngine _simulationEngine;

        public BitmapEngine( SimulationEngine simulationEngine)
        {
            _simulationEngine = simulationEngine;
        }


        public Bitmap GetBitmap()
        {
            return _simulationEngine.MapController.GetBitmap();
        }

        public Bitmap GetBitmapGrainsSelection(bool visibility)
        {
            if (visibility)
                return GetBitmap();
            else
            {
                var colorList = new List<int>();
                foreach (var grain in _simulationEngine.SelectedGrains)
                {
                    colorList.Add(grain.Id);
                }

                return _simulationEngine.MapController.GetBitmapWithHiddenColors(colorList);
            }
        }

        public Bitmap GetEnergyBitmap()
        {
            return _simulationEngine.MapController.GetBitmapWithEnergyColors();
        }

    }
}
