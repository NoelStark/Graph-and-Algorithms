

using Graph;

public class Node
{
    public string Label;
    public List<Edge> Neighbours { get; }

    public Node(string label)
    {
        this.Label = label;
        this.Neighbours = new List<Edge>();
    }

    public void AddNeighbor(Node neighbour, int weight)
    {
        Neighbours.Add(new Edge(neighbour, weight));
    }
}

