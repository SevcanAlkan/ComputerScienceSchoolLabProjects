using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class Account
    {
        public int AccountNumber
        {
            get
            {
                return Number;
            }
        }

        private int Number { get; set; }
        private string NameOfDepositor { get; set; }
        private char Type { get; set; }
        private double Balance { get; set; }

        public Account(int number, string nameOfDepositor, char type)
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

        public override string ToString()
        {
            return "@Account Number: " + this.Number.ToString() + "@Owner: " + this.NameOfDepositor + "@Type: " + (this.Type == 'C' ? "Checking" : "Saving") + "@Balance: " + this.Balance.ToString() + "@";
        }

        public bool CheckNumber(int number)
        {
            return this.Number == number;
        }       
    }
}
