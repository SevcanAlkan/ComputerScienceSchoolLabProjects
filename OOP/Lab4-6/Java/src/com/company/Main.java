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

        char input;

        do
        {
            WriteStartInfo();
            Scanner sc = new Scanner(System.in);
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

        System.out.println("--------------------------------------\n");
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
        System.out.println("All Bank Accounts;\n");

        for (Account item : BankAccounts)
        {
            System.out.println(item.toString().replace("@", "\n"));
        }
    }

    private static void AddBankAccount()
    {
        String readLineStr = "";

        int number = 0;
        String owner = "";
        char type = ' ';

        System.out.println("Add new bank account;\n");

        try
        {
            do
            {
                System.out.println("Enter Account Number:");
                Scanner sc = new Scanner(System.in);
                readLineStr = sc.nextLine();
                readLineStr = readLineStr.trim();

                if (tryParseInt(readLineStr)) {
                    number = Integer.parseInt(readLineStr);
                }else{
                    System.out.println("Account Number not suitable!\n");
                }

                final int accountNum = number;
                if (BankAccounts.stream().anyMatch((a) -> a.CheckNumber(accountNum)))
                {
                    System.out.println("Account number already taken!\n");
                    number = 0;
                }

            } while (number <= 0);

            do
            {
                System.out.println("Enter Account Owner Name:");
                Scanner sc = new Scanner(System.in);
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
                Scanner sc = new Scanner(System.in);
                type = sc.next().charAt(0);
                type = Character.toUpperCase(type);

                if (type != 'C' && type != 'S')
                    System.out.println("Account Type not suitable!\n");

            } while (type != 'C' && type != 'S');

            BankAccounts.add(new Account(number, owner, type));
            System.out.println("Account added, Number:"+number+"\n");
        }
        catch (Exception ex)
        {
            System.out.println("Operation failed!\n");
        }

    }

    private static void EditBankAccount()
    {
        String readLineStr = "";

        int number = 0;
        String owner = "";
        char type = ' ';

        System.out.println("Edit Bank Account;");
        Account rec = GetAccount();
        if (rec != null)
        {
            try
            {
                do
                {
                    System.out.println("Owner Name:");
                    Scanner sc = new Scanner(System.in);
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
                    Scanner sc = new Scanner(System.in);
                    type = sc.next().charAt(0);
                    type = Character.toUpperCase(type);

                    if (type != 'C' && type != 'S')
                        System.out.println("Account Type not suitable!\n");

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
                System.out.println("Operation failed!\n");
            }
        }
    }

    private static void RemoveBankAccount()
    {
        System.out.println("Remove Bank Account;\n");

        Account rec = GetAccount();
        if (rec != null)
        {
            char answer = ' ';

            do
            {
                System.out.println("Are you sure?(Y/N):");
                Scanner sc = new Scanner(System.in);
                answer = sc.next().charAt(0);
                answer = Character.toUpperCase(answer);
            } while (answer != 'Y' && answer != 'N');

            if (answer == 'Y')
            {
                BankAccounts.remove(rec);
                System.out.println("Account [" + rec.GetNumber() + "] deleted!\n");
            }
            else
            {
                System.out.println("Deletion process canceled!\n");
            }
        }
    }

    private static void SelectBankAccount()
    {
        System.out.println("Get Bank Account Info;\n");
        Account rec = GetAccount();
        if (rec != null)
        {
            System.out.println(rec.toString().replace("@", "\n"));
        }
    }

    private static void DepositToBankAccount()
    {
        System.out.println("Deposit To Bank Account;\n");
        Account rec = GetAccount();
        if (rec != null)
        {
            double depositAmount = 0;

            do
            {
                System.out.println("Deposit Amount:");
                Scanner sc = new Scanner(System.in);
                String amountStr = sc.nextLine();

                if (tryParseInt(amountStr)) {
                    depositAmount = Integer.parseInt(amountStr);
                }else{
                    System.out.println("Deposit amount not suitable!\n");
                }

            } while (depositAmount <= 0);

            rec.ToDeposit(depositAmount);

            System.out.println("Account new balance:["+rec.GetCurrentBalance()+"] \n");
        }
    }

    private static void WithdrawFromBankAccount()
    {
        System.out.println("Withdraw From Bank Account;\n");
        Account rec = GetAccount();
        if (rec != null)
        {
            double withdrawAmount = 0;

            do
            {
                System.out.println("Withdraw Amount:");
                Scanner sc = new Scanner(System.in);
                String amountStr = sc.nextLine();

                if (tryParseInt(amountStr)) {
                    withdrawAmount = Integer.parseInt(amountStr);
                }else{
                    System.out.println("Withdraw amount not suitable!\n");
                }

            } while (withdrawAmount <= 0);

            if ((rec.GetCurrentBalance() - withdrawAmount) >= 500)
            {
                rec.ToWithdraw(withdrawAmount);
                System.out.println("Account new balance:["+rec.GetCurrentBalance()+"] \n");
            }
            else
            {
                System.out.println("No enough money on account. The balance can't be low than 500$\n");
            }
        }
    }

    private static void GetBalanceFromBankAccount()
    {
        System.out.println("Get Bank Account Balance;\n");
        Account rec = GetAccount();
        if (rec != null)
        {
            System.out.println("Account Balance:["+rec.GetCurrentBalance()+"] \n");
        }
    }

    private static Account GetAccount()
    {
        int number = 0;

        System.out.println("Account Number:");
        Scanner sc = new Scanner(System.in);
        String numberStr = sc.nextLine();
        numberStr = numberStr.trim();

        if (tryParseInt(numberStr)) {
            number = Integer.parseInt(numberStr);
        }else{
            System.out.println("Account Number not suitable!\n");
            return null;
        }

        final int accountNum = number;
        Optional<Account> recOp = BankAccounts.stream()
                .filter(x -> x.GetNumber() == accountNum)
                .findFirst();

        if (recOp!=null)
        {
            return recOp.get();
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
