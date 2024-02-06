using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graph
{
    public class FloydWarshall
    {
        private static Dictionary<(string,string), int> distances = new Dictionary<(string, string), int>();
        public static Dictionary<(string, string), int> FloydWarShall(DirectedGraph graph)
        {
            Dictionary<string, Node> nodes = graph.GetNodes();
            int count = nodes.Count;
            var labels = nodes.Keys.ToArray();
            

            foreach (var label1 in labels)
            {
                foreach (var label2 in labels)
                {
                    distances[(label1, label2)] = label1 == label2 ? 0 : 1000000;
                }
            }

            foreach (var node in nodes.Values)
            {
                foreach(var neighbour in node.Neighbours)
                {
                    distances[(node.Label, neighbour.Node.Label)] = neighbour.Weight;
                }
            }

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < count; k++)
                    {
                        
                        int distFromi = distances[(labels[j], labels[i])] + distances[(labels[i], labels[k])];

                        if (distFromi < distances[(labels[j], labels[k])])
                        {
                            distances[(labels[j], labels[k])] = distFromi;
                        }
                    }
                }
            }

            return distances;
        }


        public static void DisplayMatrix()
        {
            Console.WriteLine("Matrix:");

            // Print column headers
            Console.Write("   ");
            var labels = distances.Keys.SelectMany(key => new[] { key.Item1 }).Distinct().ToArray();
            foreach (var label in labels)
            {
                Console.Write($"{label} ");
            }
            Console.WriteLine();

            // Print rows
            for (int i = 0; i < labels.Length; i++)
            {
                Console.Write($"{labels[i]}: ");
                for (int j = 0; j < labels.Length; j++)
                {
                    int distance;
                    if (distances.TryGetValue((labels[i], labels[j]), out distance))
                    {
                        Console.Write($"{(distance == 1000000 ? "*" : distance.ToString())} ");
                    }
                    else
                    {
                        Console.Write("*"); // If the distance doesn't exist, print "*"
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
