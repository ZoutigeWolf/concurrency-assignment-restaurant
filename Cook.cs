namespace readytogo;

internal class Cook
{
    // do you need to add variables here?
    // add the variables you need for concurrency here


    // do not add more variables after this comment.
    private readonly int id;

    public Cook(int id) // you can add more parameters if you need
    {
        this.id = id;
    }
    internal void DoWork()  // do not change the signature of this method
                            // this method is not working properly
    {
        Order? o = null;
        // each cook will ONLY get a dish from ONE order and prepare it
        o = Program.orders.First();     // do not remove this line
        Program.orders.RemoveFirst();   // do not remove this line
        
        Console.WriteLine("K: Order taken by {0}, now preparing", id);  // do not remove this line
        
        Thread.Sleep(new Random().Next(100, 500)); // do not remove this line
        // preparing an order takes time

        // when the order is ready, it is placed in the pickup location by the cook that made it.

        o.Done(); // the order is now ready
        Console.WriteLine("K: Order is: {0}", o.isReady()); // do not remove this line
        
        Program.pickups.AddFirst(o);                        // do not remove this line
        // now the client can pickup the order

        Console.WriteLine("K: Order ready");                // do not remove this line
        // each cook will terminate after preparing one order
    }
}
