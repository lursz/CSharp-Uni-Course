// using System.Collections;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace lab5
// {
//     /* ------------------------------ Thread Types ------------------------------ */
//     public class ThreadTypes
//     {
//         public int id { get; set; }
//         public static int count { get; set; }
//         public static int cycle { get; set; }

//         public Dictionary<int, int> ConsumedData = new Dictionary<int, int>();

//         public ThreadTypes()
//         {
//             count = 0;
//             cycle = 0;
//         }

//     }


    /* -------------------------------- Producer -------------------------------- */
    // public class Producer : ThreadTypes
    // {
    //     public List<int> consumed_data { get; set; }
    //     public Producer(int id)
    //     {
    //         this.id = id;
    //         consumed_data = new List<int>();
    //         count++;
    //     }

    //     public void Run()
    //     {
    //         while (ProducerConsumer.running)
    //         {

    //             Thread.Sleep(ProducerConsumer.sleepTime);
    //             ProducerConsumer.QSync.Enqueue(cycle);
    //             cycle++;

    //         }
    //     }
    // }

    // /* -------------------------------- Consumer -------------------------------- */
    // public class Consumer : ThreadTypes
    // {
    //     public Consumer(int id)
    //     {
    //         this.id = id;
    //         count++;
    //     }
    //     public void Run()
    //     {
    //         while (true)
    //         {
    //             cycle++;
    //             Thread.Sleep(ProducerConsumer.sleepTime);

    //         }
    //     }
    // }
// }