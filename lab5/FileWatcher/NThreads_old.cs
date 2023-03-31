// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace FileWatcher
// {
//     public class NThreads
//     {
//         public void start()
//         {
//             int n = 5;

//             Thread[] threads = new Thread[n];
//             Sync sync = new Sync(n);

//             for (int i = 0; i < n; i++)
//             {
//                 threads[i] = new Thread(() =>
//                 {
//                     Console.WriteLine("Wątek {0} rozpoczyna działanie.", Thread.CurrentThread.ManagedThreadId);
//                     sync.ThreadStarted();
//                     // doing sth
//                     Thread.Sleep(2000);
//                     // doiing sth
//                     Console.WriteLine("Wątek {0} kończy działanie.", Thread.CurrentThread.ManagedThreadId);
//                 });
//                 threads[i].Start();
//             }

//             sync.WaitAllThreadsStarted();

//             Console.WriteLine("Wszystkie wątki rozpoczęły działanie.");

//             // End threads
//             for (int i = 0; i < n; i++)
//             {
//                 threads[i].Join();
//             }

//             Console.WriteLine("Wszystkie wątki zakończyły działanie.");

//             Console.ReadLine();
//         }
//     }

//     class Sync
//     {
//         private int threadsCount;
//         private int threadsStarted;

//         public Sync(int threadsCount)
//         {
//             this.threadsCount = threadsCount;
//             this.threadsStarted = 0;
//         }

//         public void ThreadStarted()
//         {
//             Interlocked.Increment(ref threadsStarted);
//         }

//         public void WaitAllThreadsStarted()
//         {
//             while (threadsStarted < threadsCount)
//             {
//                 Thread.Sleep(10);
//             }
//         }
//     }
// }