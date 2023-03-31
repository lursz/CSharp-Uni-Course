
// using System;
// using System.Collections;

// class Entity {
//     protected int number = 0;

//     protected Queue ?Qsync = null;

//     protected bool shouldStop = false;
//     int timeToWait = 1000;

//     public Thread ?thread = null;
//     public Mutex ?mutex = null;

//     public int getNumber {get;}

//     public Entity(int _timeToWait, int _number, Queue Qs) {
//         number = _number;
//         Qsync = Qs;
//         timeToWait = _timeToWait;
//     }
    
//     public void StopThread() {
//         shouldStop = true;
//     }

//     public void WasteTime() {
//         Thread.Sleep(timeToWait);
//     }
// }

// class Producent: Entity {
//     public Producent(int _timeToWait, int _number, Queue Qs): base(_timeToWait, _number, Qs) {
//     }

//     public void Start() {
//         while (!shouldStop) {
//             WasteTime();

//             if (Qsync is null) {
//                 Console.WriteLine("Qsync is null");
//                 return;
//             }

//             Qsync.Enqueue(number);
//         }
//     }
// }

// class Consument: Entity {
//     Dictionary<int, int> gotStringsFromProducents = new Dictionary<int, int>();

//     public Consument(int _timeToWait, int _number, Queue Qs): base(_timeToWait, _number, Qs) {
//     }

//     private void PrintAllProducersCounts() {
//         if (mutex is null) {
//             Console.WriteLine("Mutex is null");
//             return;
//         }

//         mutex.WaitOne();

//         Console.WriteLine("Consument: " + number + " finished.");
//         foreach (var item in gotStringsFromProducents) {
//             Console.WriteLine("Producent: " + item.Key + " -> " + item.Value);
//         }

//         mutex.ReleaseMutex();
//     }

//     public void Start() {
//         Console.WriteLine("Consument: " + number + " started.");

//         while (!shouldStop) {
//             WasteTime();

//             if (Qsync is null || mutex is null) {
//                 Console.WriteLine("Qsync is null");
//                 break;
//             }

//             mutex.WaitOne();
//             if (Qsync.Count > 0) {
//                 int ?producentNumber = (int?) Qsync.Dequeue();
//                 mutex.ReleaseMutex();

//                 if (producentNumber is not null) {
//                     if (gotStringsFromProducents.ContainsKey((int) producentNumber)) {
//                         gotStringsFromProducents[(int) producentNumber]++;
//                     } else {
//                         gotStringsFromProducents.Add((int) producentNumber, 1);
//                     }
//                 }
//             } else {
//                 mutex.ReleaseMutex();
//             }
//         }

//         PrintAllProducersCounts();
//     }
// }

// class Zadanie1 {
//     public static void Z1() {
//         Queue Qsyn = Queue.Synchronized(new Queue());
//         Mutex mutex = new Mutex();
//         Random rand = new Random();

//         List<Producent> producentsT = new List<Producent>();
//         List<Consument> consumentsT = new List<Consument>();

//         Console.WriteLine("Podaj ilosc producentow: ");
//         int n = int.Parse(Console.ReadLine());

//         Console.WriteLine("Podaj ilosc konsumentow: ");
//         int m = int.Parse(Console.ReadLine());

//         for (int i = 0; i < n; i++) {
//             Producent producent = new Producent(rand.Next(100, 2000), i, Qsyn);

//             producent.thread =  (new Thread(
//                 new ThreadStart(
//                     producent.Start
//                 )
//             ));

//             producent.mutex = mutex;

//             producentsT.Add(producent);
//         }

//         for (int i = 0; i < m; i++) {
//             Consument consument = new Consument(rand.Next(50, 1000), i, Qsyn);

//             consument.thread =  (new Thread(
//                 new ThreadStart(
//                     consument.Start
//                 )
//             ));

//             consument.mutex = mutex;

//             consumentsT.Add(consument);
//         }

//         foreach (var pr in producentsT) {
//             // start producent
//             pr.thread.Start();
//         }

//         foreach (var co in consumentsT) {
//             // start consument
//             co.thread.Start();
//         }

//         while (Console.ReadKey().KeyChar != 'q');

//         foreach (var pr in producentsT) {
//             // stop producent
//             pr.StopThread();
//         }

//         foreach (var co in consumentsT) {
//             // stop consument
//             co.StopThread();
//         }
//     }
// }

// class MainClass {
//     public static void Main(String[] args) {
//         Zadanie2.Z2();
//     }
// }
