package com.company;

public class FindMin {
    int[][] globalResults;
    private static final int KEY_VALUE = 0;
    private static final int KEY_INDEX = 1;

    public void findMin(int[] arr, int threadsCount) throws java.lang.Exception {
        globalResults = new int[threadsCount][2];

        for (int i = 0; i < threadsCount; i++) {
            globalResults[i][KEY_VALUE] = Integer.MAX_VALUE;
            globalResults[i][KEY_INDEX] = -1;
        }

        Thread[] threads = new Thread[threadsCount];

        for (int threadId = 0; threadId < threads.length; threadId++) {
            int _threadId = threadId;
            threads[threadId] = new Thread(() -> {
                int from = arr.length * _threadId / threadsCount;
                int until = arr.length * (_threadId + 1) / threadsCount;
                int[] localResults = globalResults[_threadId];
                //System.out.println("Started thread(" + _threadId + ") from = " + from + ", until: " + until);
                for (int index = from; index < until; index++) {
                    if (arr[index] < localResults[KEY_VALUE]) {
                        localResults[KEY_VALUE] = arr[index];
                        localResults[KEY_INDEX] = index;
                    }
                }
                //System.out.println("Done thread(" + _threadId + ") from = " + from + ", until: " + until + ", minValue = " + localResults[KEY_VALUE] + ", minIndex = " + localResults[KEY_INDEX]);
            });

            threads[threadId].start();
        }

        for (Thread thread : threads) {
            thread.join();
        }

        int minIndex = -1;
        int minValue = Integer.MAX_VALUE;
        for (int[] threadResult : globalResults) {
            int value = threadResult[KEY_VALUE];
            if (value < minValue) {
                minValue = value;
                minIndex = threadResult[KEY_INDEX];
            }
        }

        System.out.println("Number of threads: " + threadsCount + ", min: " + minValue + ", min index:" + minIndex);
    }
}