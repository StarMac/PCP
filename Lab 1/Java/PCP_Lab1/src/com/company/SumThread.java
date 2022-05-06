package com.company;

public class SumThread extends Thread{
    private final int id;
    private final int step;
    private final BreakThread breakThread;
    private int res = 0;
    private int count = 0;

    public SumThread(int id, int step, BreakThread breakThread){
        this.id = id;
        this.step = step;
        this.breakThread = breakThread;
    }
    @Override
    public void run(){
        while (!breakThread.isCanBreak()){
            res += step;
            count++;
        }
       System.out.println("id - " + id + ", sum - " + res + ", count -  " + count);
    }
}

