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
        List<Consumer> consuments = new List<Consumer>();
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
                consuments.Add(consumer);
            }

            foreach (var i in producers)
            {
                i.Thread.Start();
            }
            foreach (var i in consuments)
            {
                i.Thread.Start();
            }


            Console.WriteLine("Press 'q' to quit.");
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                Console.WriteLine("Press 'q' to quit.");
            }

            foreach (var i in producers)
            {
                i.running = false;
            }
            foreach (var i in consuments)
            {
                i.running = false;
            }
        }
    }
}