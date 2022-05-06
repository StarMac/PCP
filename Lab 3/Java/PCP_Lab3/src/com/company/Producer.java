package com.company;

public class Producer implements Runnable{
    private final int firstId;
    private final int lastId;
    private final Manager manager;

    public Producer(int firstId, int lastId, Manager manager) {
        this.firstId = firstId;
        this.lastId = lastId;
        this.manager = manager;

        new Thread(this).start();
    }

    @Override
    public void run() {
        for (int i = firstId; i < lastId; i++) {
            try {
                manager.full.acquire();
                manager.access.acquire();

                manager.storage.add("item " + i);
                System.out.println("Producer " + Thread.currentThread().getId() + " added item " + i);

                manager.access.release();
                manager.empty.release();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}