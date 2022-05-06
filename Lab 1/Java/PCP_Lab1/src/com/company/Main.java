package com.company;

public class Main {

    public static void main(String[] args) {
        BreakThread breakThread = new BreakThread();

        new SumThread(1, 2, breakThread).start();
        new SumThread(2, 6, breakThread).start();
        new SumThread(3, 9, breakThread).start();

        new Thread(breakThread).start();
    }
}

