using System;
using System.Threading;

namespace PCP_Lab2
{
    class Program
    {
        int[,] globalResults;
        private const int KEY_VALUE = 0;
        private const int KEY_INDEX = 1;
        private int minIndex = -1;
        private int minValue = int.MaxValue;
        private readonly object locker = new object();

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
            globalResults = new int[threadsCount, 2];
            minIndex = -1;
            minValue = int.MaxValue;

            for (int i = 0; i < threadsCount; i++)
            {
                globalResults[i, KEY_VALUE] = int.MaxValue;
                globalResults[i,KEY_INDEX] = -1;
            }

            Thread [] threads = new Thread[threadsCount];

            for (int threadId = 0; threadId < threads.Length; threadId++)
            {
                int _threadId = threadId;
                threads[threadId] = new Thread(() =>
                {
                int from = arr.Length * _threadId / threadsCount;
                int until = arr.Length * (_threadId + 1) / threadsCount;
                    //Console.WriteLine("Started thread(" + _threadId + ") from = " + from + ", until: " + until);
                    for (int index = from; index < until; index++)
                {
                    if (arr[index] < globalResults[_threadId, KEY_VALUE])
                    {
                            globalResults[_threadId, KEY_VALUE] = arr[index];
                            globalResults[_threadId, KEY_INDEX] = index;
                    }
                }
                    lock (locker)
                    {
                        SetMin(_threadId);
                    }
                    //Console.WriteLine("Done thread(" + _threadId + ") from = " + from + ", until: " + until + ", minValue = " + globalResults[_threadId, KEY_VALUE] + ", minIndex = " + globalResults[_threadId, KEY_INDEX]);
                });

            threads[threadId].Start();
        }

            foreach (var item in threads)
            {
                item.Join();
            }

            Console.WriteLine($"Count of threads: {threadsCount}, min: {minValue}, minIndex: {minIndex}");
        }

        private void SetMin(int threadId)
        {
            int value = globalResults[threadId, KEY_VALUE];
            if (value < minValue)
            {
                minValue = value;
                minIndex = globalResults[threadId, KEY_INDEX];
            }
        }

    }
}