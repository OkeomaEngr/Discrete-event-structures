//COIS 2020H Assignment 2
//William Harper, 0674584
//Amber Ahmed, 0680481
//Okeoma Nwabueze, 0659928

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PriorityQueue
{
    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty
        int Size();        // Return the number of items in an instance
    }

    //-----------------------------------------------------------------------------

    public interface IPriorityQueue<T> : IContainer<T> where T : IComparable
    {
        void Add(T item);  // Add an item to a priority queue
        void Remove();     // Remove the item with the highest priority
        T Front();         // Return the item with the highest priority
    }

    //-------------------------------------------------------------------------

    // Priority Queue
    // Implementation:  Binary heap

    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable
    {
        private int capacity;  // Maximum number of items in a priority queue
        private T[] A;         // Array of items
        private int count;     // Number of items in a priority queue

        // Constructor 1
        // Create a priority with maximum capacity of size
        // Time complexity:  O(1)

        public PriorityQueue(int size)
        {
            capacity = size;
            A = new T[size + 1];  // Indexing begins at 1
            count = 0;
        }

        // Constructor 2
        // Create a priority from an array of items
        // Time complexity:  O(n)

        public PriorityQueue(T[] inputArray)
        {
            int i;

            count = capacity = inputArray.Length;
            A = new T[capacity + 1];

            for (i = 0; i < capacity; i++)
            {
                A[i + 1] = inputArray[i];
            }

            BuildHeap();
        }

        // PercolateUp
        // Percolate up an item from position i in a priority queue
        // Time complexity:  O(log n)

        private void PercolateUp(int i)
        {
            int child = i, parent;

            // As long as child is not the root (i.e. has a parent)
            while (child > 1)
            {
                parent = child / 2;
                if (A[child].CompareTo(A[parent]) > 0)
                // If child has a higher priority than parent
                {
                    // Swap parent and child
                    T item = A[child];
                    A[child] = A[parent];
                    A[parent] = item;
                    child = parent;  // Move up child index to parent index
                }
                else
                    // Item is in its proper position
                    return;
            }
        }

        // Add
        // Add an item into the priority queue
        // Time complexity:  O(log n)

        public void Add(T item)
        {
            if (count < capacity)
            {
                A[++count] = item;  // Place item at the next available position
                PercolateUp(count);
            }
        }

        // PercolateDown
        // Percolate down from position i in a priority queue
        // Time complexity:  O(log n)

        private void PercolateDown(int i)
        {
            int parent = i, child;

            // while parent has at least one child
            while (2 * parent <= count)
            {
                // Select the child with the highest priority
                child = 2 * parent;    // Left child index
                if (child < count)  // Right child also exists
                    if (A[child + 1].CompareTo(A[child]) > 0)
                        // Right child has a higher priority than left child
                        child++;

                // If child has a higher priority than parent
                if (A[child].CompareTo(A[parent]) > 0)
                {
                    // Swap parent and child
                    T item = A[child];
                    A[child] = A[parent];
                    A[parent] = item;
                    parent = child;  // Move down parent index to child index
                }
                else
                    // Item is in its proper place
                    return;
            }
        }

        // Remove
        // Remove an item from the priority queue
        // Time complexity:  O(log n)

        public void Remove()
        {
            if (!Empty())
            {
                // Remove item with highest priority (root) and
                // replace it with the last item
                A[1] = A[count--];

                // Percolate down the new root item
                PercolateDown(1);
            }
        }

        // Front
        // Return the item with the highest priority
        // Time complexity:  O(1)

        public T Front()
        {
            if (!Empty())
            {
                return A[1];  // Return the root item (highest priority)
            }
            else
                return default(T);
        }

        // BuildHeap
        // Create a binary heap from a given list
        // Time complexity:  O(n)

        private void BuildHeap()
        {
            int i;

            // Percolate down from the last parent to the root (first parent)
            for (i = count / 2; i >= 1; i--)
            {
                PercolateDown(i);
            }
        }

        // HeapSort
        // Sort and return inputArray
        // Time complexity:  O(n log n)

        public void HeapSort(T[] inputArray)
        {
            int i;

            capacity = count = inputArray.Length;

            // Copy input array to A (indexed from 1)
            for (i = capacity - 1; i >= 0; i--)
            {
                A[i + 1] = inputArray[i];
            }

            // Create a binary heap
            BuildHeap();

            // Remove the next item and place it into the input (output) array
            for (i = 0; i < capacity; i++)
            {
                inputArray[i] = Front();
                Remove();
            }
        }

        // MakeEmpty
        // Reset a priority queue to empty
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            count = 0;
        }

        // Empty
        // Return true if the priority is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Return the number of items in the priority queue
        // Time complexity:  O(1)

        public int Size()
        {
            return count;
        }


        // Peek
        // Return the item at index i in the priority queue
        public T Peek(int i)
        {
            if (!Empty())
            {
                return A[i];
            }
            else
                return default(T);

        }

        // Remove At
        // Remove the item at index i in the priority queue
        public void RemoveAt(int i)
        {
            if (!Empty())
            {
                A[i] = A[count--];

                // Percolate down the new root item
                PercolateDown(i);
            }
        }
    }
}


//----------------------------------------------------------------

// Common interface for ALL linear data structures

//-------------------------------------------------------------------------

// Queue
// Behavior:  First-In, First-Out (FIFO)
// Implementation:  Circular Array

public interface IContainer<T>
{
    void MakeEmpty();  // Reset an instance to empty
    bool Empty();      // Test if an instance is empty 
    int Size();        // Return the number of items in an instance
}

//-------------------------------------------------------------------------

public interface IQueue<T> : IContainer<T>
{
    void Enqueue(T item);  // Add an item to the back of a queue
    void Dequeue();        // Remove an item from the front of a queue
    T Front();             // Return the front item of a queue
}

//-------------------------------------------------------------------------

// Queue
// Behavior:  First-In, First-Out (FIFO)
// Implementation:  Circular Array

public class Queue<T> : IQueue<T>
{
    private int capacity;     // Maximum capacity of the queue
    private int count;        // Actual number of items in the queue
    private T[] A;            // Linear array of items (Generic)
    private int front, back;  // Indices of the front and back items

    // Time complexity of all methods: O(1)

    // Constructor
    // Creates an empty queue with a capacity of size

    public Queue(int size)
    {
        capacity = size;
        A = new T[size];
        MakeEmpty();
    }

    public Queue() : this(100) { }  // Invokes Queue(100)

    // Enqueue
    // Inserts an item at the back of the queue
    // If the queue is full then nothing is done

    public void Enqueue(T item)
    {
        if (count < capacity)
        {
            back = (back + 1) % capacity;    // Increment back with wraparound 
            A[back] = item;
            count++;
        }
    }

    // Dequeue
    // Removes the front item of the queue
    // If the queue is empty then nothing is done

    public void Dequeue()
    {
        if (!Empty())
        {
            front = (front + 1) % capacity;  // Increment front with wraparound
            count--;
        }
    }

    // Front
    // Retrieves the front item of the queue
    // Returns default(T) if the queue is empty

    public T Front()
    {
        if (!Empty())
            return A[front];
        else
            return default(T);
    }

    // MakeEmpty
    // Resets the queue to empty

    public void MakeEmpty()
    {
        front = 0;
        count = 0;
        back = capacity - 1;
    }

    // Empty
    // Returns true if the queue is empty; false otherwise

    public bool Empty()
    {
        return count == 0;
    }

    // Size
    // Returns the number of items in the queue

    public int Size()
    {
        return count;
    }
}


namespace DiscreteEventSimulation
{

    // Simulation
    // Main class for the program, controlling the simulation
    public class Simulation
    {
        public PriorityQueue.PriorityQueue<Event> priorityqueue;   // A priority queue of events to process
        public Queue<Event> waitingqueue;                          // The waiting queue that events enter when processors are full
        bool[] processors;                                         // Boolean array of processsors
        int pCount;                                                // The number of processors
        Random r;                                                  // The random number generator used in RunSimulation    
        int time;                                                  // The current time in seconds of the processor, RunSimulation goes until 3600 seconds
        bool CreateChronology;                                     // If true, a 1 page chronololgy of events is created
        int ChronologyCount;                                       // Tracks how many events are recorded in the chronology, limits the total to 24

        // Constructor
        public Simulation()
        {
            priorityqueue = new PriorityQueue.PriorityQueue<Event>(3600);
            waitingqueue = new Queue<Event>(3600);

        }

        //PerformEvent
        //Takes an event from the priority queue and processes it
        private void PerformEvent()
        {
            if (!priorityqueue.Empty())
            {

                Event curr = priorityqueue.Front(); //Store the event from the top of the priority queue

                if (curr.IsArrival)
                {
                    priorityqueue.Remove();

                    if (curr.Job.JobNumber != 0)
                    {
                        //arrival event

                        if (CreateChronology && ChronologyCount < 24)
                        {
                            ChronologyCount++;
                            Console.WriteLine("arrival at " + time / 3600 + ":" + (time % 3600) / 60 + ":" + time % 60);
                        }

                        bool jobAdded = false;


                        //check for an empty processor
                        for (int i = 0; i < pCount; i++)
                        {
                            if (processors[i])
                            {
                                processors[i] = false;

                                //create a departure event for this job, and add it to the priority queue
                                Event e = new Event(time + curr.Job.RemainingTime, false, curr.Job, i);
                                priorityqueue.Add(e);
                                jobAdded = true;

                                break;
                            }

                        }

                        if (!jobAdded)
                        {
                            //place in the waiting queue if no processor is found
                            curr.waitTime = time;
                            waitingqueue.Enqueue(curr);
                        }

                    }
                    else
                    {
                        //interupt

                        if (CreateChronology && ChronologyCount < 24)
                        {
                            ChronologyCount++;
                            Console.WriteLine("interupt " + time / 3600 + ":" + (time % 3600) / 60 + ":" + time % 60);
                        }

                        //select a random processor to interupt
                        double u = r.NextDouble() * pCount;
                        int pInterupted = Convert.ToInt32(u);
                        if (pInterupted >= pCount)
                            pInterupted--;
                        bool interuptFailed = false;

                        for (int i = 0; i < priorityqueue.Size(); i++)
                        {

                            //find the departure event for the interupted job
                            Event peek = priorityqueue.Peek(i + 1);
                            if (peek.Job.JobNumber != 0)
                            {
                                interuptFailed = true;
                                break;
                            }

                            if (peek.IsArrival == false)
                            {
                                if (peek.Processor == pInterupted)
                                {
                                    priorityqueue.RemoveAt(i);
                                    bool pAvailable = false;

                                    //find a new processor for the interupted event
                                    for (int j = 0; j < pCount; j++)
                                    {
                                        if (j != pInterupted)
                                        {
                                            if (processors[j])
                                            {
                                                peek.Processor = j;
                                                priorityqueue.Add(peek);
                                                pAvailable = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (!pAvailable)
                                    {
                                        //place the interupted job in the waiting queue if no processor is found
                                        peek.Job.RemainingTime = peek.Time - time;
                                        peek.waitTime = time;
                                        waitingqueue.Enqueue(peek);
                                        
                                    }

                                    break;
                                }
                            }
                        }

                        if (!interuptFailed)
                        {
                            //interupt cannot interupt an interupt
                            processors[pInterupted] = false;
                            Event e = new Event(time + curr.Job.RemainingTime, false, curr.Job, pInterupted);
                            priorityqueue.Add(e);
                        }
                    }
                }
                else
                {
                    //departure event
                    if (time >= curr.Time)
                    {
                        if (CreateChronology && ChronologyCount < 24)
                        {
                            ChronologyCount++;
                            Console.WriteLine("departure at " + time / 3600 + ":" + (time % 3600) / 60 + ":" + time % 60);
                        }

                        //remove the departure event, and free the processor
                        priorityqueue.Remove();
                        processors[curr.Processor] = true;

                    }
                }

            }
        }

        //RunSimulation
        //Runs the Discrete Event Simulation once
        //M is the mean arrival time between jobs
        //T is the mean time that each job takes to execute
        //P is the number of processors
        public double RunSimulation(int M, int T, int P, bool createChronology)
        {
            int waitTime = 0;

            pCount = P;
            time = 0;
            r = new Random();
            processors = new bool[P];
            CreateChronology = createChronology;
            ChronologyCount = 0;

            for(int i=0; i<P; i++)
            {
                processors[i] = true;
            }

            bool isDone = false;
            int jobArrive = 0;
            int jobNumber = 1;

            while (!isDone)
            {
                if (time < 3600)
                {
                    //add new jobs
                    if (jobArrive == 0)
                    {

                        Job job = new Job();

                        //create a random number
                        double u = r.NextDouble();

                        //decide whether to create an interupt or not
                        if (u < 0.9)
                        {
                            job.JobNumber = jobNumber;
                            jobNumber++;
                        }
                        else
                            job.JobNumber = 0;


                        //decide the time for this job to complete
                        u = r.NextDouble();
                        u = -1 * (T * Math.Log(u)); // T ln(u)
                        job.RemainingTime = Convert.ToInt32(u) + 1;


                        //decide the time before the next job comes in
                        u = r.NextDouble();
                        u = -1 * (M * Math.Log(u)); // M ln(u)
                        jobArrive = Convert.ToInt32(u) + 1;


                        //place the new arrival into the priority queue
                        Event e = new Event(time, true, job);
                        priorityqueue.Add(e);


                    }
                    else
                    {
                        //countdown until the next event comes in
                        jobArrive--;
                    }
                }

                //remove from waiting queue

                for (int i=0; i<pCount; i++)
                {
                    if (processors[i])
                    {
                        if (!waitingqueue.Empty())
                        {
                            Event curr = waitingqueue.Front();
                            waitingqueue.Dequeue();

                            //the time spent waiting by the event is added to waitTime
                            waitTime += time - curr.waitTime;

                            if (curr.IsArrival)
                            {
                                processors[i] = false;
                                Event e = new Event(time + curr.Job.RemainingTime, false, curr.Job, i);
                                priorityqueue.Add(e);
                            }
                            else
                            {
                                curr.Time = time + curr.Job.RemainingTime;
                                priorityqueue.Add(curr);
                            }
                        }
                        else
                            break;
                    }
                }

                //remove from priority queue

                PerformEvent();

                if (time >= 3600 && priorityqueue.Empty() && waitingqueue.Empty())
                {
                    isDone = true;
                }

                time++;

            }

            if (CreateChronology)
                Console.WriteLine("");

            //return the average wait time
            return waitTime / (double)jobNumber;

        }
    }

    // Job
    // Stores the job number and remaining time of a job
    public class Job
    {
        public int JobNumber;                  // The job number for the job, if 0, it is an interupt
        public int RemainingTime;              // The remaining time for the job to complete

    }

    // Event
    // Stores data for each event, and implements IComparable for the priority queue
    public class Event : IComparable
    {
        public Job Job;                        // reference to the Job being performed by the event
        public int Processor;                  // the index of the processor being used, only set for departures
        public bool IsArrival = true;          // if true is an arrival, if false is a departure
        public int Time;                       // priorityValue
        public int waitTime;                   // time that the Event has waited in the WaitingQueue

        // Constructor
        // Create an event at time, as an arrival or departure, with the given Job
        public Event(int time, bool isArrival, Job job)
        {
            Time = time;
            IsArrival = isArrival;
            Job = job;
            waitTime = 0;
        }

        // Constructor
        // Create an event at time, as an arrival or departure, with the given Job, at the given processor
        public Event(int time, bool isArrival, Job job, int processor)
        {
            Time = time;
            IsArrival = isArrival;
            Job = job;
            Processor = processor;
            waitTime = 0;
        }

        // CompareTo (inherited from IComparable)
        // Returns >0 if the current item is greater than obj (null or otherwise)
        // Returns 0  if the current item is equal to obj (of PriorityClass)
        // Returns <0 if the current item is less than obj (of PriorityClass)

        public int CompareTo(Object obj)
        {
            if (obj != null)
            {
                Event other = (Event)obj;   // Explicit cast
                if (other != null)
                    return other.Time - Time;
                else
                    return 1;
            }
            else
                return 1;
        }

    }

    class Program
    {

        // Main
        // Calculates the average wait time for 1-10 processors
        // For light, medium, and heavy loads
        // And creates 6 pages of the chronology of the program
        static void Main()
        {
            Simulation sim = new Simulation();

            //results

            for (int t = 1; t <= 4; t *= 2)
            {
                if (t==1)
                    Console.WriteLine("Light Load : T = (M * P / 2)");
                else if (t==2)
                    Console.WriteLine("Medium Load : T = (M * P)");
                else
                    Console.WriteLine("Heavy Load : T = (M * P * 2)");

                Console.WriteLine("");

                for (int p = 1; p <= 10; p++)
                {
                    double totalWaitTime = 0.0;
                    for (int j = 0; j < 20; j++)
                        totalWaitTime += Math.Round(sim.RunSimulation(10, 10 * p * t / 2, p, false), 2);
                    double averageWaitTime = totalWaitTime / 20.0;
                    Console.WriteLine("Average wait time for " + (p) + " processors: " + Math.Round(averageWaitTime, 2));
                }
                Console.WriteLine("");
            }

            //chronologies

            Console.WriteLine("");

            for (int t = 1; t <= 4; t *= 2)
            {
                if (t == 1)
                    Console.WriteLine("Light Load : T = (M * P / 2)");
                else if (t == 2)
                    Console.WriteLine("Medium Load : T = (M * P)");
                else
                    Console.WriteLine("Heavy Load : T = (M * P * 2)");

                Console.WriteLine("");


                Console.WriteLine("");
                double l1 = sim.RunSimulation(10, 10 * 1 * t / 2, 1, true);
                Console.WriteLine("Average wait time for " + (1) + " processors: " + Math.Round(l1, 2));
                Console.WriteLine("");
                double l10 = sim.RunSimulation(10, 10 * 10 * t / 2, 10, true);
                Console.WriteLine("Average wait time for " + (10) + " processors: " + Math.Round(l10, 2));
                Console.WriteLine("");
            }

            


            Console.ReadLine();
        }
    }
}
