using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiscaleModelling.Models
{
    public class Map
    {
        private Node[,] _nodes;
        public Map(int width, int height)
        {
            _nodes = new Node[width, height];
        }

        public Node GetNode(int x, int y)
        {
            return _nodes[x, y];
        }
        public void SetNode (Node node, int x, int y)
        {
            _nodes[x, y] = node;
        }


    }
}
