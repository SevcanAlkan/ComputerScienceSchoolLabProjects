package com.company;

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        int num = 0;

        System.out.println("Which times table do you want?");
        Scanner sc = new Scanner(System.in);
        num = sc.nextInt();

        if(num <= 0)
            return;

        for(int i = 1; i < 11; i++){
            System.out.println( i + " times " + num + "=" + (i*num));
        }

        System.out.print("Press any key to continue . . . ");
        sc.nextLine();
    }
}
