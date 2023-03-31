using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5
{
    public class Ex1
    {
        Random random = new Random();
        List<Producer> producers = new List<Producer>();
        List<Consumer> consumers = new List<Consumer>();
        List<int> data = new List<int>();
        int m;
        int n;

        public Ex1(int m_, int n_)
        {
            m = m_;
            n = n_;
        }

        public void Start()
        {



            for (int i = 0; i < n; i++)
            {
                Producer producer = new Producer(i, data, random.Next(100, 2000));
                producer.Thread = new Thread(new ThreadStart(producer.Start));
                producers.Add(producer);
            }
            for (int i = 0; i < m; i++)
            {
                Consumer consumer = new Consumer(i, data, random.Next(50, 1000));
                consumer.Thread = new Thread(new ThreadStart(consumer.Start));
                consumers.Add(consumer);
            }
            foreach (var i in producers)
            {
                i.Thread.Start();
                System.Console.WriteLine("Producer " + i.number + " started");
            }
            foreach (var i in consumers)
            {
                i.Thread.Start();
                System.Console.WriteLine("Consumer " + i.number + " started");
            }
            


            Console.WriteLine("Press 'q' to quit.");
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                Console.WriteLine("Press 'q' to quit.");
            }
            Stop();
            // print read resources
            
            
        }

        public void Stop()
        {
            foreach (var i in producers)
            {
                i.running = false;
                System.Console.WriteLine("Producer " + i.number + " stopped");
            }
            foreach (var i in consumers)
            {
                i.running = false;
                System.Console.WriteLine("Consumer " + i.number + " stopped");
            }
        }
    }
}



