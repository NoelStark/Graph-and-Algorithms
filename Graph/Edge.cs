using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Edge
    {
        public int Weight { get; }
        public Node Node { get; }

        public Edge(Node node, int weight)
        {
            this.Weight = weight;
            this.Node = node;
        }
    }

}
