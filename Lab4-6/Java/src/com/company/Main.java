package com.company;
import java.util.ArrayList;
import java.util.Optional;
import java.util.Scanner;
import java.awt.event.KeyEvent;

public class Main {

    public static ArrayList<Account> BankAccounts;

    public static void main(String[] args) {

        BankAccounts = new ArrayList<Account>();

        BankAccounts.add(new Account(1, "Sevcan alkan", 'S'));
        BankAccounts.add(new Account(2, "John Smith", 'S'));
        BankAccounts.add(new Account(3, "Marco Diaz", 'C'));
        BankAccounts.add(new Account(4, "Ertan Mutlu", 'C'));

        Scanner sc = new Scanner(System.in);
        char input;

        do
        {
            WriteStartInfo();
            input = sc.next().charAt(0);
            input = Character.toUpperCase(input);

            switch (input)
            {
                case 'L':
                    ListAllBankAccount();
                    break;
                case 'A':
                    AddBankAccount();
                    break;
                case 'E':
                    EditBankAccount();
                    break;
                case 'R':
                    RemoveBankAccount();
                    break;
                case 'S':
                    SelectBankAccount();
                    break;
                case 'D':
                    DepositToBankAccount();
                    break;
                case 'W':
                    WithdrawFromBankAccount();
                    break;
                case 'B':
                    GetBalanceFromBankAccount();
                    break;
                default:
                    break;
            }
        } while (input != 'Q');
    }

    private static void WriteStartInfo()
    {
        System.out.println("!!!Press 'Q' for exit!!!");
        System.out.println("Please select what to do you want;");
        System.out.println("List all bank accounts(Press L)");
        System.out.println("Add bank account(Press A)");
        System.out.println("Edit bank account(Press E)");
        System.out.println("Remove bank account(Press R)");
        System.out.println("Select bank account(Press S)");
        System.out.println("Add deposit to a bank account(Press D)");
        System.out.println("Withdraw from a bank account(Press W)");
        System.out.println("Get balance a bank account(Press B)");
    }

    private static void ListAllBankAccount()
    {
        System.out.println("\nAll Bank Accounts;");

        for (Account item : BankAccounts)
        {
            System.out.println(item.toString().replace("@", "\n"));
        }
        System.out.println("--------------------------------------");
    }

    private static void AddBankAccount()
    {
        Scanner sc = new Scanner(System.in);
        String readLineStr = "";

        int number = 0;
        String owner = "";
        char type = ' ';

        System.out.println("\nAdd new bank account;");

        try
        {
            do
            {
                System.out.println("Enter Account Number:");
                readLineStr = sc.nextLine();
                readLineStr = readLineStr.trim();

                if (tryParseInt(readLineStr)) {
                    number = Integer.parseInt(readLineStr);
                }else{
                    System.out.println("Account Number not suitable!\n");
                }

                if (BankAccounts.stream().anyMatch((a) -> a.CheckNumber(number)))
                {
                    System.out.println("Account Number already taken!\n");
                    number = 0;
                }

            } while (number <= 0);

            do
            {
                System.out.println("Enter Account Owner Name:");
                readLineStr = sc.nextLine();
                readLineStr = readLineStr.trim();
                owner = readLineStr;

                if (owner == "")
                    System.out.println("Account Owner Name not suitable!\n");
            } while (owner == "");

            System.out.println("Enter Account Type;");
            System.out.println("C=Checking");
            System.out.println("S=Saving");
            do
            {

                System.out.println("Type(C OR S):");
                type = sc.next().charAt(0);
                type = Character.toUpperCase(type);

                if (type != 'C' && type != 'S')
                    System.out.println("\nAccount Type not suitable!");

            } while (type != 'C' && type != 'S');

            BankAccounts.add(new Account(number, owner, type));
            System.out.println("Account added, Number:"+number+"\n");
        }
        catch (Exception ex)
        {
            System.out.println("Operation failed!");
        }

        System.out.println("--------------------------------------");
    }

    private static void EditBankAccount()
    {
        Scanner sc = new Scanner(System.in);
        String readLineStr = "";

        int number = 0;
        String owner = "";
        char type = ' ';

        System.out.println("\nEdit Bank Account;");
        Account rec = GetAccount();
        if (rec != null)
        {
            try
            {
                do
                {
                    System.out.println("Owner Name:");
                    readLineStr = sc.nextLine();
                    readLineStr = readLineStr.trim();
                    owner = readLineStr;

                    if (owner == "")
                        System.out.println("Account Owner Name not suitable!\n");
                } while (owner == "");

                System.out.println("Enter Account Type;");
                System.out.println("C=Checking");
                System.out.println("S=Saving");
                do
                {

                    System.out.println("Type(C OR S):");
                    type = sc.next().charAt(0);
                    type = Character.toUpperCase(type);

                    if (type != 'C' && type != 'S')
                        System.out.println("\nAccount Type not suitable!");

                } while (type != 'C' && type != 'S');

                BankAccounts.remove(rec);
                Account NewRec = new Account(rec.GetNumber(), owner, type);
                NewRec.ToDeposit(rec.GetCurrentBalance());
                BankAccounts.add(NewRec);

                System.out.println("\nAccount edited!");
                System.out.println(NewRec.toString().replace("@", "\n"));
            }
            catch (Exception ex)
            {
                System.out.println("Operation failed!");
            }
        }

        System.out.println("--------------------------------------");
    }

    private static void RemoveBankAccount()
    {
    }

    private static void SelectBankAccount()
    {
    }

    private static void DepositToBankAccount()
    {
    }

    private static void WithdrawFromBankAccount()
    {
    }

    private static void GetBalanceFromBankAccount()
    {

    }

    private static Account GetAccount()
    {
        Scanner sc = new Scanner(System.in);

        int number = 0;

        System.out.println("\nAccount Number:");
        String numberStr = sc.nextLine();
        numberStr = numberStr.trim();

        if (tryParseInt(numberStr)) {
            number = Integer.parseInt(numberStr);
        }else{
            System.out.println("Account Number not suitable!\n");
        }

        Optional<Account> recOp = BankAccounts.stream()
                .filter(x -> x.GetNumber() == number)
                .findFirst();

        Account rec = null;

        if(recOp.isPresent()) {
            rec = recOp.get();
        }

        if (rec==null)
        {
            return rec;
        }else{
            System.out.println("We coudn't find bank account! Please check bank account with List function.\n");
            return null;
        }
    }

    private static boolean tryParseInt(String value) {
        try {
            Integer.parseInt(value);
            return true;
        } catch (NumberFormatException e) {
            return false;
        }
    }
}
