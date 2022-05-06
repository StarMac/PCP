package com.company;

public class FindMin {
    private int min = Integer.MAX_VALUE;

        public void FindMin(int[] arr, int threadsCount) throws java.lang.Exception
        {
            Thread[] threads = new Thread[threadsCount];

            min = Integer.MAX_VALUE;

            for (int threadId = 0; threadId < threads.length; threadId++)
            {
                int _threadId = threadId;
                threads[threadId] = new Thread(() ->
                {
                    for (int i = arr.length * _threadId / threadsCount; i < arr.length *
                            (_threadId + 1) / threadsCount; i++)
                    {
                            setMin(arr, i);
                    }
                });

                threads[threadId].run();
            }
            for (Thread thread : threads)
            {
                thread.join();
            }

            System.out.println("Number of threads: " + threadsCount + ", min: " + min);
        }

        synchronized private void setMin(int[] arr, int index){
            if (arr[index] < min)
            {
                min = arr[index];
            }
        }
}