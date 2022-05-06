package com.company;

import java.util.Random;

public class ArrClass {
    public void Start() throws java.lang.Exception
    {
        FindMin findMin = new FindMin();
        int[] arr = new int[10000000];
        Random random = new Random();
        for (int i = 0; i < arr.length; i++)
        {
            arr[i] = random.nextInt();
        }

        findMin.FindMin(arr, 3);
        findMin.FindMin(arr, 6);
        findMin.FindMin(arr, 8);
        findMin.FindMin(arr, 10);
    }
}
