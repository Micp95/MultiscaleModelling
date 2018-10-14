using MultiscaleModelling.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Helpers
{
    public class FileHelper
    {
        private static Random _random = new Random();
        public static void ExportToBmp(Bitmap map,string fileName)
        {
            map.Save(fileName);
        }
        public static void ExportToTxt(Map map, string fileName)
        {
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.WriteLine(string.Format("{0} {1} 1", map.Width, map.Height));
                for (int x = 0; x < map.Width; x++)
                {
                    for (int y = 0; y < map.Height; y++)
                    {
                        var node = map.GetNode(x, y);
                        writetext.WriteLine(string.Format("{0} {1} {2} {3}", node.X,node.Y,(int)node.Type,node.Id));
                    }
                }
            }
        }

        public static Map ImportFromBmp(string fileName,Dictionary<Color,TypeEnum> specialColors)
        {
            var nodeList = new List<Node>();
            var bitmap = new Bitmap(fileName);
            var width = bitmap.Width;
            var height = bitmap.Height;
            int currentGrainId = 100;

            var rerMap = new Map(width, height);
            for (int x = 0; x < rerMap.Width; x++)
            {
                for (int y = 0; y < rerMap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var newNode = new Node()
                    {
                        X = x,
                        Y = y,
                        Color = Color.FromArgb(pixel.ToArgb())
                    };

                    //is border/empty etc.
                    if (specialColors.Any(k => k.Key.ToArgb() == pixel.ToArgb()))
                    {
                        newNode.Type = specialColors.FirstOrDefault(k => k.Key.ToArgb() == pixel.ToArgb()).Value;
                    }else//is grain
                    {
                        newNode.Type = TypeEnum.Grain;

                        var savedNode = nodeList.FirstOrDefault(k => k.Color.ToArgb() == pixel.ToArgb());
                        if(savedNode != null)
                        {
                            newNode.Id = savedNode.Id;
                        }else
                        {
                            newNode.Id = currentGrainId++;
                        }
                    }


                    rerMap.SetNode(newNode, x, y);
                }
            }
            return rerMap;
        }
        public static Map ImportFromTxt(string fileName)
        {
            var nodeList = new List<Node>();
            Map resMap = null;

            using (StreamReader readerFile = new StreamReader(fileName))
            {
                var firstLine = readerFile.ReadLine();
                
                var mapConfigNumbers = Array.ConvertAll<string, int>(firstLine.Split(), int.Parse);

                var width = mapConfigNumbers[0];
                var height = mapConfigNumbers[1];

                resMap = new Map(width, height);

                while (!readerFile.EndOfStream)
                {
                    var line = readerFile.ReadLine();
                    var nodeConfigNumbers = Array.ConvertAll<string, int>(line.Split(), int.Parse);
                    int x = nodeConfigNumbers[0];
                    int y = nodeConfigNumbers[1];
                    var newNode = new Node()
                    {
                        X = x,
                        Y = y,
                        Type = (TypeEnum)nodeConfigNumbers[2],
                        Id = nodeConfigNumbers[3],
                    };

                    var savedNode = nodeList.FirstOrDefault(k => k.Id == newNode.Id);
                    if(savedNode == null)
                    {
                        var randomColor = Color.FromArgb(_random.Next(2, 254), _random.Next(2, 254), _random.Next(2, 254));
                        newNode.Color = randomColor;
                        nodeList.Add(newNode);
                    }else
                    {
                        newNode.Color = savedNode.Color;
                    }

                    resMap.SetNode(newNode, x, y);
                }

            }
            return resMap;
        }
    }
}
