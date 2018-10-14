using MultiscaleModelling.Helpers;
using MultiscaleModelling.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Controllers
{
    public class MapController
    {
        private static Color _borderColor = Color.Black;
        private static Color _emptyColor = Color.White;

        private int _width;
        private int _height;

        private Map _previousMap;
        private Map _currentMap;


        public MapController()
        {

        }
        public MapController(int width, int height)
        {
            //add border nodes
            width++;
            height++;

            _width = width;
            _height = height;
            _currentMap = new Map(_width, _height);
            Commit();
        }

        public void Commit()
        {
            _previousMap = _currentMap;
            CreteNewMap();
        }

        public void Rollback()
        {
            CreteNewMap();
        }

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public Node GetNode (int x, int y)
        {
            return _previousMap.GetNode(x, y);
        }
        public List<Node> GetNeighbourhoods(int x, int y, NeighbourhoodEnum type)
        {
            switch (type)
            {
                case NeighbourhoodEnum.Moore:
                    return GetMooreNeighbourhoods(x,y);
                case NeighbourhoodEnum.VonNeumann:
                    return GetVonNeumannNeighbourhoods(x,y);
            }
            return new List<Node>();
        }

        public void SetNode(int x, int y, Node node)
        {
            _currentMap.SetNode(node, x, y);
        }

        public Node GetCurrentNode (int x,int y)
        {
            return _currentMap.GetNode(x, y);
        }

        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(_width, _height);
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var node = _previousMap.GetNode(x, y);
                    if (node.Type == TypeEnum.Empty)
                        bitmap.SetPixel(x, y, _emptyColor);
                    else if(node.Type == TypeEnum.Grain)
                        bitmap.SetPixel(x, y, _previousMap.GetNode(x, y).Color);
                    else if(node.Type == TypeEnum.Border)
                        bitmap.SetPixel(x, y, _borderColor);
                }
            }
            return bitmap;
        }


        public void ImportFromFile(string name,FileTypeEnum type)
        {
            switch (type)
            {
                case FileTypeEnum.Bmp:
                    var mapper = GetColorMapper();
                    _currentMap = FileHelper.ImportFromBmp(name, mapper);
                    break;
                case FileTypeEnum.Text:
                    _currentMap = FileHelper.ImportFromTxt(name);
                    break;
            }

            _width = _currentMap.Width;
            _height = _currentMap.Height;

            Commit();
        }
        public void ExportToFile(string name, FileTypeEnum type)
        {
            switch (type)
            {
                case FileTypeEnum.Bmp:
                    FileHelper.ExportToBmp(GetBitmap(),name);
                    break;
                case FileTypeEnum.Text:
                    FileHelper.ExportToTxt(_previousMap, name);
                    break;
            }
        }


        private Dictionary<Color, TypeEnum> GetColorMapper()
        {
            var result = new Dictionary<Color, TypeEnum>();

            result.Add(_borderColor, TypeEnum.Border);
            result.Add(_emptyColor, TypeEnum.Empty);

            return result;
        }
        private void CreteNewMap()
        {
            _currentMap = new Map(_width, _height);
            CreateEmptyMap();
            AddBitmapBorder();
        }
        private void CreateEmptyMap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _currentMap.SetNode(new Node()
                    {
                        X = x,
                        Y = y,
                        Type = TypeEnum.Empty
                    }, x, y);
                }
            }
        }
        private void AddBitmapBorder()
        {
            for(int k = 0; k < _width; k++)
            {
                _currentMap.SetNode(new Node()
                {
                    X = k,
                    Y = 0,
                    Type = TypeEnum.Border
                },k,0);
                _currentMap.SetNode(new Node()
                {
                    X = k,
                    Y = _height - 1,
                    Type = TypeEnum.Border
                }, k, _height - 1);
            }

            for (int k = 0; k < _height; k++)
            {
                _currentMap.SetNode(new Node()
                {
                    X = 0,
                    Y = k,
                    Type = TypeEnum.Border
                }, 0, k);
                _currentMap.SetNode(new Node()
                {
                    X = _width - 1,
                    Y = k,
                    Type = TypeEnum.Border
                }, _width - 1, k);
            }
        }
        private List<Node> GetMooreNeighbourhoods(int x, int y)
        {
            List<Node> neighbourhoods = new List<Node>();
            for(int k = -1; k <= 1; k++)
            {
                neighbourhoods.Add(_previousMap.GetNode(x + k, y + 1));
                neighbourhoods.Add(_previousMap.GetNode(x + k, y - 1));
                if(k != 0)
                {
                    neighbourhoods.Add(_previousMap.GetNode(x + k, y));
                }
            }

            return neighbourhoods;
        }
        private List<Node> GetVonNeumannNeighbourhoods(int x, int y)
        {
            List<Node> neighbourhoods = new List<Node>();

            neighbourhoods.Add(_previousMap.GetNode(x , y+1));
            neighbourhoods.Add(_previousMap.GetNode(x-1, y));
            neighbourhoods.Add(_previousMap.GetNode(x+1, y));
            neighbourhoods.Add(_previousMap.GetNode(x, y-1));

            return neighbourhoods;
        }
    }
}
