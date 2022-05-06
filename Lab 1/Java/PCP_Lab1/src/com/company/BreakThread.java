package com.company;

import java.util.Random;

public class BreakThread extends Thread{
    private boolean canBreak  = false;

    @Override
    public void run() {
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        canBreak  = true;
    }

    public synchronized boolean isCanBreak() {
        return canBreak;
    }
}
