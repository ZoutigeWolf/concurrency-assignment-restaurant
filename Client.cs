namespace readytogo;

internal class Client
{
    // add the variables you need for concurrency here
    public Thread thread;

    // do not add more variables after this comment.
    private readonly int id = 0;

    public Client(int id)
    {
        this.id = id;
        this.thread = new Thread(DoWork);
    }


    internal void Start()
    {
        thread.Start();
    }

    internal void DoWork()
    {
        // wait before placing order
        Thread.Sleep(new Random().Next(100, 500)); // do not remove this line
   
        Order o = new();

        lock (Program.orders)
        {
            //place the order
            Program.orders.AddFirst(o);  // do not remove this line
            
            // notify waiting cook
            Monitor.Pulse(Program.orders);
        }

        Console.WriteLine("C: Order placed by {0}", id); // do not remove this line
        
        // sleep for a bit
        Thread.Sleep(new Random().Next(100, 500));  // do not remove this line

        lock (Program.pickups)
        {
            // wait for pickup to become available
            while (Program.pickups.Count == 0)
            {
                Monitor.Wait(Program.pickups);
            }
            
            // pickup the order
            Program.pickups.RemoveFirst(); // do not remove this line
        }

        Console.WriteLine("C: Order pickedup by {0}", id); // do not remove this line
    }
}