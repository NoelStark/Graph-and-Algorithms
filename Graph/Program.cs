using System;
using System.Collections.Generic;
using System.Linq;
using Graph;


public class Program
{
    static void Main(string[] args)
    {
        DirectedGraph graph = new DirectedGraph();
        List<string> nodes = new List<string>();
        for (char c = 'A'; c <= 'D'; c++)
        {
            graph.AddNode(c.ToString());
            nodes.Add(c.ToString());
        }

        graph.AddWeightedEdge("A", "B", 2);
        graph.AddWeightedEdge("A", "C", 5);
        graph.AddWeightedEdge("B", "C", 1);
        graph.AddWeightedEdge("B", "D", 4);
        graph.AddWeightedEdge("C", "D", 3);


        graph.DisplayGraph();
        graph.DisplayAdjacencyMatrix();
        Node source = graph.GetNode("A");
        Console.WriteLine();

        Dictionary<Node, Dictionary<Node, int>> allDistances = new();

        foreach (var item in nodes)
        {
            Node sourceNode = graph.GetNode(item);

            Dictionary<Node, int>distances = DijkstraAlgorithm.RunDijkstra(graph, sourceNode);
            allDistances[sourceNode] = distances;
            
        }
        DijkstraAlgorithm.DisplayDijkstra(allDistances);

    }
}
