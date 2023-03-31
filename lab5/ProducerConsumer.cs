using System;
using System.Collections;
using System.Threading.Tasks;

namespace lab5
{
    /* -------------------------------------------------------------------------- */
    /*                              ProducerConsumer                              */
    /* -------------------------------------------------------------------------- */
    public class ProducerConsumer
    {
        public int number;
        public List<int> data { get; set; }
        public Thread Thread = null;
        public int sleepTime { get; set; }

        public ProducerConsumer(int number_, List<int> data_, int sleepTime_)
        {
            this.number = number_;
            this.data = data_;
            this.sleepTime = sleepTime_;
        }



    }








    /* -------------------------------------------------------------------------- */
    /*                                  Producer                                  */
    /* -------------------------------------------------------------------------- */
    public class Producer : ProducerConsumer
    {
        public bool running = true;

        public Producer(int number_, List<int> data_, int sleepTime_): base(number_, data_, sleepTime_)
        {}

        public void Start()
        {
            while (running)
            {
                Thread.Sleep(sleepTime);
                WriteResource();
            }
        }

        public void WriteResource()
        {
            // mutex.WaitOne();
            lock (data)
            {
                this.data.Add(number);
            }
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                                  Consumer                                  */
    /* -------------------------------------------------------------------------- */
    public class Consumer : ProducerConsumer
    {
        public bool running { get; set; }
        public List<int> consumed { get; set; }

        public Consumer(int number_, List<int> data_, int sleepTime_): base(number_, data_, sleepTime_)
        {
            consumed = new List<int>();
            running = false;
        }

        public void Start()
        {
            while (!running)
            {
                Thread.Sleep(sleepTime);
                ReadResource();
            }
            Console.WriteLine($"Consumer: {number} has just read: {ToString()}");
        }

        public void ReadResource()
        {
            lock (data)
            {
                if (data.Count == 0) return;
                consumed.Add(data[data.Count - 1]);
                data.RemoveAt(data.Count - 1);
            }
        }

        public override string ToString()
        {
            return string.Join(" ", consumed);
        }
    }




}






