using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class Program
    {
        public static List<Account> BankAccounts = new List<Account>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bank Account Manager!");
            char tmpSelection = ' ';

            BankAccounts.Add(new Account(1, "Sevcan alkan", 'S'));
            BankAccounts.Add(new Account(2, "John Smith", 'S'));
            BankAccounts.Add(new Account(3, "Marco Diaz", 'C'));
            BankAccounts.Add(new Account(4, "Ertan Mutlu", 'C'));

            do
            {
                while (!Console.KeyAvailable)
                {
                    WriteStartInfo();
                    tmpSelection = Console.ReadKey().KeyChar;

                    switch (Char.ToUpper(tmpSelection))
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
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void WriteStartInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Please select what to do you want;");
            Console.WriteLine("List all bank accounts(Press L)");
            Console.WriteLine("Add bank account(Press A)");
            Console.WriteLine("Edit bank account(Press E)");
            Console.WriteLine("Remove bank account(Press R)");
            Console.WriteLine("Select bank account(Press S)");
            Console.WriteLine("Add deposit to a bank account(Press D)");
            Console.WriteLine("Withdraw from a bank account(Press W)");
            Console.WriteLine("Get balance a bank account(Press B)");
        }

        private static void ListAllBankAccount()
        {
            Console.WriteLine("\nAll Bank Accounts;");
            foreach (var item in BankAccounts)
            {
                Console.Write(item.ToString().Replace("@", System.Environment.NewLine));
            }
            Console.WriteLine("--------------------------------------");
        }

        private static void AddBankAccount()
        {
            string readLineStr = "";

            int number = 0;
            string owner = "";
            char type = ' ';

            Console.WriteLine("\nAdd new bank account;");

            try
            {
                do
                {
                    Console.Write("Enter Account Number:");
                    readLineStr = Console.ReadLine();
                    readLineStr = readLineStr.Trim();

                    if (!int.TryParse(readLineStr, out number) || number <= 0)
                        Console.WriteLine("Account Number not suitable!\n");

                    if (BankAccounts.Any(a => a.CheckNumber(number)))
                    {
                        Console.WriteLine("Account Number already taken!\n");
                        number = 0;
                    }

                } while (number <= 0);

                do
                {
                    Console.Write("Enter Account Owner Name:");
                    readLineStr = Console.ReadLine();
                    readLineStr = readLineStr.Trim();
                    owner = readLineStr;

                    if (owner == "")
                        Console.WriteLine("Account Owner Name not suitable!\n");
                } while (owner == "");

                Console.WriteLine("Enter Account Type;");
                Console.WriteLine("C=Checking");
                Console.WriteLine("S=Saving");
                do
                {

                    Console.Write("Type(C OR S):");
                    type = Console.ReadKey().KeyChar;
                    type = char.ToUpper(type);

                    if (type != 'C' && type != 'S')
                        Console.WriteLine("\nAccount Type not suitable!");

                } while (type != 'C' && type != 'S');

                BankAccounts.Add(new Account(number, owner, type));
                Console.WriteLine("Account added, Number:{0}", number);
            }
            catch (Exception)
            {
                Console.WriteLine("Operation failed!");
            }

            Console.WriteLine("--------------------------------------");
        }

        private static void EditBankAccount()
        {
            string readLineStr = "";

            int number = 0;
            string owner = "";
            char type = ' ';

            Console.WriteLine("\nEdit Bank Account;");
            Account rec = GetAccount();
            if (rec != null)
            {
                try
                {
                    do
                    {
                        Console.Write("Owner Name:");
                        readLineStr = Console.ReadLine();
                        readLineStr = readLineStr.Trim();
                        owner = readLineStr;

                        if (owner == "")
                            Console.WriteLine("Account Owner Name not suitable!\n");
                    } while (owner == "");

                    Console.WriteLine("Enter Account Type;");
                    Console.WriteLine("C=Checking");
                    Console.WriteLine("S=Saving");
                    do
                    {

                        Console.Write("Type(C OR S):");
                        type = Console.ReadKey().KeyChar;
                        type = char.ToUpper(type);

                        if (type != 'C' && type != 'S')
                            Console.WriteLine("\nAccount Type not suitable!");

                    } while (type != 'C' && type != 'S');

                    BankAccounts.Remove(rec);
                    Account NewRec = new Account(rec.AccountNumber, owner, type);
                    NewRec.ToDeposit(rec.GetCurrentBalance());
                    BankAccounts.Add(NewRec);

                    Console.WriteLine("\nAccount edited!");
                    Console.Write(NewRec.ToString().Replace("@", System.Environment.NewLine));
                }
                catch (Exception)
                {
                    Console.WriteLine("Operation failed!");
                }
            }

            Console.WriteLine("--------------------------------------");
        }

        private static void RemoveBankAccount()
        {
            Console.WriteLine("\nRemove Bank Account;");
            Account rec = GetAccount();
            if (rec != null)
            {
                char answer = ' ';

                do
                {
                    Console.Write("\nAre you sure?(Y/N):");
                    answer = Console.ReadKey().KeyChar;
                    answer = char.ToUpper(answer);
                } while (answer != 'Y' && answer != 'N');

                if (answer == 'Y')
                {
                    BankAccounts.Remove(rec);
                    Console.WriteLine("\nAccount [{0}] deleted!\n", rec.AccountNumber);
                }
                else
                {
                    Console.WriteLine("\nDeletion process canceled!\n");
                }
            }

            Console.WriteLine("--------------------------------------");
        }

        private static void SelectBankAccount()
        {
            Console.WriteLine("\nGet Bank Account Info;");
            Account rec = GetAccount();
            if (rec != null)
            {
                Console.Write(rec.ToString().Replace("@", System.Environment.NewLine));
            }

            Console.WriteLine("--------------------------------------");
        }

        private static void DepositToBankAccount()
        {
            Console.WriteLine("\nDeposit To Bank Account;");
            Account rec = GetAccount();
            if (rec != null)
            {
                double depositAmount = 0;

                do
                {
                    Console.Write("Deposit Amount:");
                    string amountStr = Console.ReadLine();

                    if (!double.TryParse(amountStr, out depositAmount) || depositAmount <= 0)
                        Console.WriteLine("\nDeposit amount not suitable!\n");

                } while (depositAmount <= 0);

                rec.ToDeposit(depositAmount);

                Console.WriteLine("Account new balance:[{0}] \n", rec.GetCurrentBalance());
            }

            Console.WriteLine("--------------------------------------");
        }

        private static void WithdrawFromBankAccount()
        {
            Console.WriteLine("\nWithdraw From Bank Account;");
            Account rec = GetAccount();
            if (rec != null)
            {
                double withdrawAmount = 0;

                do
                {
                    Console.Write("Withdraw Amount:");
                    string amountStr = Console.ReadLine();

                    if (!double.TryParse(amountStr, out withdrawAmount) || withdrawAmount <= 0)
                        Console.WriteLine("\nWithdraw amount not suitable!\n");

                } while (withdrawAmount <= 0);

                if ((rec.GetCurrentBalance() - withdrawAmount) >= 500)
                {
                    rec.ToWithdraw(withdrawAmount);
                    Console.WriteLine("Account new balance:[{0}] \n", rec.GetCurrentBalance());
                }
                else
                {
                    Console.WriteLine("\nNo enough money on account. The balance can't be low than 500$\n");
                }
            }

            Console.WriteLine("--------------------------------------");
        }

        private static void GetBalanceFromBankAccount()
        {
            Console.WriteLine("\nGet Bank Account Balance;");
            Account rec = GetAccount();
            if (rec != null)
            {
                Console.WriteLine("Account Balance:[{0}] \n", rec.GetCurrentBalance());
            }

            Console.WriteLine("--------------------------------------");
        }

        private static Account GetAccount()
        {
            int number = 0;

            Console.Write("\nAccount Number:");
            string numberStr = Console.ReadLine();
            numberStr = numberStr.Trim();

            if (!int.TryParse(numberStr, out number) || number <= 0)
                Console.WriteLine("\nAccount Number not suitable!\n");

            if (BankAccounts.Any(a => a.CheckNumber(number)))
            {
                Account rec = BankAccounts.Find(a => a.AccountNumber == number);

                return rec;
            }
            else
            {
                Console.WriteLine("We coudn't find bank account! Please check bank account with List function.\n");
                return null;
            }
        }
    }
}
