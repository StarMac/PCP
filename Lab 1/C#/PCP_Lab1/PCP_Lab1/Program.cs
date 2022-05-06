using System;
using System.Threading;

namespace PCP_Lab1
{
    class Program
    {
        static private Random rnd = new Random();
        static void Main(string[] args)
        {
            new Thread(() => MainThread()).Start();
        }
        static void MainThread()
        {
            new Thread(() =>
            {
                Sum(1,3);
            }).Start();

            new Thread(() =>
            {
                Sum(2, 2);
            }).Start();

            new Thread(() =>
            {
                Sum(3, 4);
            }).Start();

            new Thread(Stoper).Start();
        }

        static private bool canStop = false;

        public bool CanStop { get => canStop; }

        static public void Stoper()
        {
            Thread.Sleep(500);
            canStop = true;
        }

        static void Sum(int id, int step)
        {
            int res = 0;
            int count = 0;
            while(!canStop)
            {
                res += step;
                count++;
            }
            Console.WriteLine("id - {0}, sum - {1}, count - {2}", id, res, count);
        }
    }
}
