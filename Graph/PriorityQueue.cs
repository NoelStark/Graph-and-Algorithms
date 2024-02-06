using Graph;

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