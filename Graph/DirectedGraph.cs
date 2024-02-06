using Graph;

public class DirectedGraph
{
    private Dictionary<string, Node> nodes = new();

    public Node GetNode(string name)
    {
        return nodes[name];
    }

    public void AddNode(string label)
    {
        if (!nodes.ContainsKey(label))
            nodes[label] = new Node(label);
    }

    public void AddWeightedEdge(string fromLabel, string toLabel, int weight)
    {
        if (nodes.ContainsKey(fromLabel) && nodes.ContainsKey(toLabel))
        {
            Node fromNode = nodes[fromLabel];
            Node toNode = nodes[toLabel];
            fromNode.AddNeighbor(toNode, weight);
        }
    }

    public void DisplayGraph()
    {
        foreach (var node in nodes.Values)
        {
            foreach (var neighbour in node.Neighbours)
            {
                Console.WriteLine($"{node.Label} -> {neighbour.Node.Label} has weight of {neighbour.Weight}");
            }
        }
    }

    public void DisplayAdjacencyMatrix()
    {
        Console.WriteLine("Matrix");

        var labels = nodes.Keys.ToArray();

        Console.Write("   ");
        foreach (var label in labels)
        {
            Console.Write($"{label} ");
        }

        Console.WriteLine();

        foreach (var fromLabel in labels)
        {
            Console.Write($"{fromLabel}: ");
            foreach (var toLabel in labels)
            {
                var fromNode = nodes[fromLabel];
                var toNode = nodes[toLabel];
                var edge = fromNode.Neighbours.FirstOrDefault(x => x.Node == toNode);
                if (fromNode == toNode)
                    Console.Write("0 ");
                else
                    Console.Write($"{(edge != null ? edge.Weight.ToString() : "*")} ");
            }

            Console.WriteLine();
        }
    }

    public Dictionary<string, Node> GetNodes()
    {
        return nodes;
    }
}