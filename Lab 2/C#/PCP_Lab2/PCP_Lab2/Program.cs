using System;
using System.Threading;

namespace PCP_Lab2
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }

        private void Start()
        {
            int [] arr = new int[10000000];
            Random random = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next() - 1000;
            }

            FindMin(arr, 3);
            FindMin(arr, 6);
            FindMin(arr, 8);
            FindMin(arr, 10);
        }
        private void FindMin(int[] arr, int threadsCount)
        {
            Thread [] threads = new Thread[threadsCount];

            int min = int.MaxValue;
            int minIndex = -1;
            object block = new object();

            for (int threadId = 0; threadId < threads.Length; threadId++)
            {
                int _threadId = threadId;
                threads[threadId] = new Thread(() =>
                {
                    for (int i = arr.Length * _threadId / threadsCount; i < arr.Length *
                    (_threadId + 1) / threadsCount; i++)
                    {
                        lock (block)
                        {
                            if (arr[i] < min)
                            {
                                min = arr[i];
                                minIndex = i;
                            }
                        }
                    }
                });

                threads[threadId].Start();
            }

            foreach (var item in threads)
            {
                item.Join();
            }

            Console.WriteLine($"Count of threads: {threadsCount}, min: {min}, minIndex: {minIndex}");
        }
    }
}