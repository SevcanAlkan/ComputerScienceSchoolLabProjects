using Lab9.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public static class SampleDataInitializer
    {
        public static Tuple<List<BookStore>, List<Serial>, List<BookStoreSerial>> Run()
        {
            List<BookStore> Stores = new List<BookStore>();
            List<Serial> Serials = new List<Serial>();
            List<BookStoreSerial> Stocks = new List<BookStoreSerial>();

            #region Create Sample Data

            Stores.AddRange(new List<BookStore>() {
                new BookStore() { Id = 1, Name ="D&R" },
                new BookStore() { Id = 2, Name ="BKM" },
                new BookStore() { Id = 3, Name ="Google Books" }
            });

            Serials.AddRange(new List<Serial>() {
                new Serial() { Id = 1, Title="Top Gear", Publisher="BBC", PlaceOfPublication="England", Price=5,
                    Editions = new List<DateTime>(){ new DateTime(2019,1,1), new DateTime(2019, 2, 1), new DateTime(2019, 3, 1) } },
                new Serial() { Id = 2, Title="Popular Science", Publisher ="Hardward Press", PlaceOfPublication="England", Price=9,
                    Editions = new List<DateTime>(){ new DateTime(2019,1,1), new DateTime(2019, 3, 1), new DateTime(2019, 3, 1) }  },
                new Serial() { Id = 3, Title="Developer Montly", Publisher="Microsoft", PlaceOfPublication="USA", Price=12,
                    Editions = new List<DateTime>(){ new DateTime(2019,1,1), new DateTime(2019, 3, 1) } },
            });

            Stocks.AddRange(new List<BookStoreSerial>() {
                new BookStoreSerial() {  Serial = Serials[0], Edition = Serials[0].Editions[0], AmountOfCopy = 1, BookStoreId=1, MarkupPercentage= 0 },
                new BookStoreSerial() {  Serial = Serials[0], Edition = Serials[0].Editions[1], AmountOfCopy = 4, BookStoreId=1, MarkupPercentage= 10 },
                new BookStoreSerial() {  Serial = Serials[0], Edition = Serials[0].Editions[2], AmountOfCopy = 20, BookStoreId=1, MarkupPercentage= 50 },

                new BookStoreSerial() {  Serial = Serials[2], Edition = Serials[2].Editions[1], AmountOfCopy = 15, BookStoreId=2, MarkupPercentage= 10 },
                new BookStoreSerial() {  Serial = Serials[1], Edition = Serials[1].Editions[2], AmountOfCopy = 15, BookStoreId=2, MarkupPercentage= 10 },

                new BookStoreSerial() {  Serial = Serials[2], Edition = Serials[2].Editions[1], AmountOfCopy = 20, BookStoreId=3, MarkupPercentage= 10 },
            });

            #endregion

            return Tuple.Create(Stores, Serials, Stocks);
        }
    }
}
