package com.company;

import java.util.Random;

public class Main {

    public static void main(String[] args) throws java.lang.Exception {
        FindMin findMin = new FindMin();
        int[] arr = new int[10000000];
        Random random = new Random();
        for (int i = 0; i < arr.length; i++) {
            arr[i] = random.nextInt();
        }

        findMin.findMin(arr, 3);
        findMin.findMin(arr, 6);
        findMin.findMin(arr, 8);
        findMin.findMin(arr, 10);
    }
}
