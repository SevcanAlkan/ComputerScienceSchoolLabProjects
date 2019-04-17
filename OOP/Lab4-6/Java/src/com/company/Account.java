package com.company;

public class Account {

    public int GetNumber()
    {
        return Number;
    }

    private int Number;
    private String NameOfDepositor;
    private char Type;
    private double Balance;

    public Account(int number, String nameOfDepositor, char type)
    {
        this.Number = number;
        this.NameOfDepositor = nameOfDepositor;
        this.Type = type;
    }

    public double ToDeposit(double amount)
    {
        if (amount <= 0)
            return this.Balance;

        this.Balance = this.Balance + amount;

        return this.Balance;
    }

    public double ToWithdraw(double amount)
    {
        if (amount <= 0)
            return this.Balance;

        this.Balance = this.Balance - amount;

        return this.Balance;
    }

    public double GetCurrentBalance()
    {
        return this.Balance;
    }

        @Override
    public String toString() {
        return String.format("@Account Number: " + this.Number + "@Owner: " + this.NameOfDepositor + "@Type: " + (this.Type == 'C' ? "Checking" : "Saving") + "@Balance: " + this.Balance + "@");
    }

    public boolean CheckNumber(int number)
    {
        return this.Number == number;
    }
}
