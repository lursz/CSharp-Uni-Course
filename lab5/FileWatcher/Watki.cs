using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileWatcher
{
    
public class Worker
{
    private Manager manager;
    public Worker(Manager manager)
    {
        this.manager = manager;
    }
    public void Start()
    {
        Interlocked.Increment(ref manager.active_threads);
        manager.EwhWorker.WaitOne();
        Interlocked.Decrement(ref manager.active_threads);
        manager.EwhManager.Set();
    }
}
public class Manager
{
    
        public long active_threads;
        public long all_threads;
        public EventWaitHandle EwhWorker;
        public EventWaitHandle EwhManager;

        public Manager (long all_threads)
        {
            this.all_threads = all_threads;
            this.active_threads = 0;
            this.EwhWorker = new EventWaitHandle(false, EventResetMode.AutoReset);
            this.EwhManager = new EventWaitHandle(false, EventResetMode.AutoReset);
        }


        public void Start()
        {
            for (var i = 0; i < all_threads; i++)
                new Thread(new Worker(this).Start).Start();

            while (Interlocked.Read(ref active_threads) != all_threads)
                Thread.Sleep(100);

            System.Console.WriteLine("All threads started");

            while (Interlocked.Read(ref active_threads) > 0)
                WaitHandle.SignalAndWait(EwhWorker, EwhManager);

            System.Console.WriteLine("All threads finished");
        }




















}

}