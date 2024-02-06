using System;
using System.Collections.Generic;
using System.Linq;

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

public class Graph
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

public class DijkstraAlgorithm
{
    public static Dictionary<Node, int> RunDijkstra(Graph graph, Node source)
    {
        var distances = new Dictionary<Node, int>();
        var priorityQueue = new PriorityQueue();
        foreach (var node in graph.GetNodes().Values)
        {
            distances[node] = int.MaxValue; //Cannot give an actual value until vertices are explored
        }

        distances[source] = 0;
        priorityQueue.Enqueue(source, 0);
        while (priorityQueue.Count > 0) //While there are elements in the queue
        {
            var (currentNode, currentDistance) = priorityQueue.Dequeue(); //Remove the top element of the queue

            foreach (var neighborEdge in currentNode.Neighbours) //Checks all the Edges for the current nodes neighbors
            {
                var neighborNode = neighborEdge.Node; //First neighbor found
                var newDistance = currentDistance + neighborEdge.Weight; //Here we get a new distance between the nodes

                if (newDistance >= distances[neighborNode]) continue; //if the new distance is bigger (the road is longer)
                distances[neighborNode] = newDistance; //Otherwise we found a faster way to get to a node
                priorityQueue.Enqueue(neighborNode, newDistance); //We put the neighborNode and its new distance into the queue
            }
        }
        return distances;
    }

    public static void DisplayDijkstra(Dictionary<Node, Dictionary<Node, int>> distances)
    {
        Console.WriteLine("Shortest Path Matrix");
        Console.Write("  ");
        foreach (var node in distances.Keys)
        {
            Console.Write($"{node.Label} ");
        }

        Console.WriteLine();

        foreach (var row in distances.Keys)
        {
            Console.Write($"{row.Label} ");

            foreach (var col in distances.Keys)
            {
                int distance = distances[row][col];
                if (distance != int.MaxValue)
                {
                    if (row == col)
                        Console.Write("0 ");
                    else
                    {
                        Console.Write($"{distance} ");
                    }
                }
                else
                    Console.Write("\u221e ");

                
            }

            Console.WriteLine();
        }
    }
}

class PriorityQueue
{
    private List<(Node, int)> heap = new();
    public int Count => heap.Count;

    /// <summary>
    /// The method that adds the elements to the heap
    /// </summary>
    /// <param name="node">Passes in the node that is to be added</param>
    /// <param name="distance">Passes in the current distance (this is whats used for comparing in the heapify methods)</param>
    public void Enqueue(Node node, int distance)
    {
        heap.Add((node, distance));
        HeapifyUp();
    }
    /// <summary>
    /// Method that removes elements from the queue
    /// </summary>
    /// <returns></returns>
    public (Node, int) Dequeue()
    {
        if(heap.Count == 0)
            Console.WriteLine("Queue is empty");
      
        var top = heap[0]; //Grabs the top item
        heap[0] = heap[^1]; //same as heap[heap.Count -1]. Called end expression
        heap.RemoveAt(heap.Count -1); //Removes the item
        HeapifyDown(); //Adjusts the queue
        return top; //returns the top item
        
    }
    /// <summary>
    /// A method that adjusts the heap
    /// </summary>
    public void HeapifyUp()
    {
        int index = heap.Count - 1;
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2; //Gets the index of the parent in the heap
            if (heap[parentIndex].Item2 > heap[index].Item2) //if the value in the parent is larger than the child
            {
                Swap(parentIndex, index); //The child moves up
                index = parentIndex;
            }
            else
                break;
        }
    }
    /// <summary>
    /// The other metod that adjusts the heap
    /// </summary>
    public void HeapifyDown()
    {
        int index = 0;
        while (true)
        {
            int leftChildIndex = 2 * index + 1; //Gets the left child of the Node
            int rightChildIndex = 2* index + 2; //Gets the right child of the Node
            int smallest = index;

            if (leftChildIndex < heap.Count && heap[leftChildIndex].Item2 < heap[smallest].Item2) //if the left child is smaller
            {
                smallest = leftChildIndex;
            }

            if (rightChildIndex < heap.Count && heap[rightChildIndex].Item2 < heap[smallest].Item2) //If the right child is smaller
            {
                smallest = rightChildIndex;
            }

            if (smallest != index)
            {
                Swap(index, smallest); //if something has changed in the two if statements, we need to switch the elements
                index = smallest;
            }
            else
                break;
        }
    }
    /// <summary>
    /// The method that swaps elements
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    public void Swap(int i, int j)
    {
        (heap[i], heap[j]) = (heap[j], heap[i]); //This works the same as the one below but with deconstruction
        /*
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
        */
    }
}



public class Program
{
    static void Main(string[] args)
    {
        Graph graph = new Graph();
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
