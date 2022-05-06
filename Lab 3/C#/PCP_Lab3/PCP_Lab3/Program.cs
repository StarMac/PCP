using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PCP_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program(4, 20, 5, 12);

            Console.ReadKey();
        }

        private Program(int storageSize, int itemNumber, int producers, int consumers)
        {
            Access = new Semaphore(1, 1);
            Full = new Semaphore(storageSize, storageSize);
            Empty = new Semaphore(0, storageSize);

            for (int i = 0; i < consumers - 1; i++)
            {
                (new Thread(Consumer)).Start(itemNumber / consumers);
            }


            (new Thread(Consumer)).Start(itemNumber - itemNumber / consumers * (consumers - 1));

            for (int i = 0; i < producers - 1; i++)
            {
                (new Thread(Producer)).Start(itemNumber / producers);
            }

            (new Thread(Producer)).Start(itemNumber - itemNumber / producers * (producers - 1));
        }

        private readonly Semaphore Access;
        private readonly Semaphore Full;
        private readonly Semaphore Empty;

        private readonly List<string> storage = new();

        private void Producer(Object itemNumber)
        {
            int maxItem = (itemNumber as int?) ?? 0;

            for (int i = 0; i < maxItem; i++)
            {
                Full.WaitOne();
                Thread.Sleep(60);
                Access.WaitOne();

                storage.Add($"item {i} ({Environment.CurrentManagedThreadId})");
                Console.WriteLine($"Producer id {Environment.CurrentManagedThreadId} added item {i}");

                Access.Release();
                Empty.Release();
            }

            Console.WriteLine($"Producer id {Environment.CurrentManagedThreadId} finished");
        }

        private void Consumer(Object itemNumber)
        {
            int maxItem = (itemNumber as int?) ?? 0;

            for (int i = 0; i < maxItem; i++)
            {
                Empty.WaitOne();
                Thread.Sleep(100);
                Access.WaitOne();

                string item = storage.ElementAt(0);
                storage.RemoveAt(0);
                Console.WriteLine($"Consumer id {Environment.CurrentManagedThreadId} removed item {item}");
                Full.Release();
                Access.Release();
            }

            Console.WriteLine($"Consumer id {Environment.CurrentManagedThreadId} finished");
        }
    }
}