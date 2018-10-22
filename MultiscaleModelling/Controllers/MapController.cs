﻿using MultiscaleModelling.Helpers;
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
        private static Color _inclusionColor = Color.Black;

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

        public void CopyMap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _currentMap.SetNode(_previousMap.GetNode(x, y),x,y);
                }
            }
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
            if(x >= 0 && y >= 0 && x < _width && y < _height)
                _currentMap.SetNode(node, x, y);
        }

        public Node GetCurrentNode (int x,int y)
        {
            if (x < _width && y < _height)
                return _currentMap.GetNode(x, y);
            return null;
        }

        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(_width, _height);
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var node = _previousMap.GetNode(x, y);
                    if (node == null || node.Type == TypeEnum.Empty)
                        bitmap.SetPixel(x, y, _emptyColor);
                    else if(node.Type == TypeEnum.Grain)
                        bitmap.SetPixel(x, y, _previousMap.GetNode(x, y).Color);
                    else if(node.Type == TypeEnum.Border)
                        bitmap.SetPixel(x, y, _borderColor);
                    else if (node.Type == TypeEnum.Inclusion)
                        bitmap.SetPixel(x, y, _inclusionColor);
                }
            }
            return bitmap;
        }


        public void ImportFromFile(string name,FileTypeEnum type)
        {
            var mapper = GetColorMapper();
            switch (type)
            {
                case FileTypeEnum.Bmp:
                    _currentMap = FileHelper.ImportFromBmp(name, mapper);
                    break;
                case FileTypeEnum.Text:
                    _currentMap = FileHelper.ImportFromTxt(name, mapper);
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


        private Dictionary<TypeEnum,Color> GetColorMapper()
        {
            var result = new Dictionary<TypeEnum,Color>();

            result.Add(TypeEnum.Border,_borderColor );
            result.Add(TypeEnum.Inclusion,_inclusionColor);
            result.Add(TypeEnum.Empty,_emptyColor);

            return result;
        }
        private void CreteNewMap()
        {
            _currentMap = new Map(_width, _height);
            CreateEmptyMap();
        }
        private void CreateEmptyMap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    TypeEnum type = TypeEnum.Empty;
                    if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                        type = TypeEnum.Border;
                    _currentMap.SetNode(new Node()
                    {
                        X = x,
                        Y = y,
                        Type = type
                    }, x, y);
                }
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
