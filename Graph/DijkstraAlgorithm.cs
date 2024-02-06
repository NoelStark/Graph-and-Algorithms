using Graph;

public class DijkstraAlgorithm
{
    public static Dictionary<Node, int> RunDijkstra(DirectedGraph graph, Node source)
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