using Lab9.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9
{
    public class Program
    {
        private static List<BookStore> Stores;
        private static List<Serial> Serials;
        private static List<BookStoreSerial> Stocks;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");
            string tmpSelection = " ";
            int selection = 0;

            var data = SampleDataInitializer.Run();
            Stores = data.Item1 != null ? data.Item1 : new List<BookStore>();
            Serials = data.Item2 != null ? data.Item2 : new List<Serial>();
            Stocks = data.Item3 != null ? data.Item3 : new List<BookStoreSerial>();

            do
            {
                while (!Console.KeyAvailable)
                {
                    WriteStartInfo();
                    tmpSelection = Console.ReadLine();
                    int.TryParse(tmpSelection, out selection);

                    switch (selection)
                    {
                        case 1:
                            Console.WriteLine("List Of Stores;");
                            Core.PrintList<BookStore>(Stores);
                            break;
                        case 2:
                            Console.WriteLine("List Of Serials;");
                            Core.PrintList<Serial>(Serials);
                            break;
                        case 3:
                            GetSerialListOfStore();
                            break;
                        case 4:
                            GetSerialInfoFromStore();
                            break;
                        case 5:
                            StoreDetail();
                            break;
                        case 6:
                            SerialDetail();
                            break;
                        case 7:
                            EditStoreStock();
                            break;
                        case 8:
                            PrintLowestSerialStock();
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine("--------------------------------------\n");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void WriteStartInfo()
        {
            Console.WriteLine("\nPlease select what to do you want;");
            Console.WriteLine("List All Stores(Press 1)");
            Console.WriteLine("List All Serials(Press 2)");
            Console.WriteLine("Get Serial List Of Store(Press 3)");
            Console.WriteLine("Get Serial Info From Store(Press 4)");
            Console.WriteLine("Add/Edit/Remove Store(Press 5)");
            Console.WriteLine("Add/Edit/Remove Serial(Press 6)");
            Console.WriteLine("Add/Remove Serial Stock To Store(Press 7)");
            Console.WriteLine("Get Lowest Serial Stock Of Store(Press 8)\n");
        }

        private static void GetSerialListOfStore()
        {
            Console.WriteLine("List Serials Of Store;");

            int number = 0;

            number = Core.ReadInt("Store Id:");

            if (Stocks.Any(a => a.BookStoreId == number))
            {
                var list = Stocks.Where(a => a.BookStoreId == number).ToList();

                Core.PrintList<BookStoreSerial>(list);
            }
        }

        private static void GetSerialInfoFromStore()
        {
            Console.WriteLine("Serial Info;");

            int storeId = 0, serialId = 0;

            storeId = Core.ReadInt("Store Id:");
            serialId = Core.ReadInt("Serial Id:");

            if (Stocks.Any(a => a.BookStoreId == storeId && a.Serial.Id == serialId))
            {
                var rec = Stocks.Where(a => a.BookStoreId == storeId && a.Serial.Id == serialId).FirstOrDefault();

                Core.Print(rec);
            }
            else
            {
                Console.WriteLine("\nThe Serial not found!\n");
            }
        }

        private static void StoreDetail()
        {
            Console.WriteLine("Store Add/Edit/Remove;");

            Console.WriteLine("Plese select what do you want to do: A,E,R");
            char input = Char.ToUpper(Console.ReadKey().KeyChar);

            BookStore rec = new BookStore();
            int storeId = 0;

            switch (input)
            {
                case 'A':

                    rec.Id = Stores.Select(a => a.Id).OrderByDescending(o => o).FirstOrDefault() + 1;

                    Console.WriteLine("New store ID is: {0}", rec.Id);
                    rec.Name = Core.ReadStr("Please input store name:");

                    Console.WriteLine("The new store is: {0}", rec.ToString().Replace("@", System.Environment.NewLine));
                    Console.WriteLine("Are you confirm to create new store? (Y/N)");
                    char confirmChr = Char.ToUpper(Console.ReadKey().KeyChar);

                    if (confirmChr == 'Y')
                        Stores.Add(rec);
                    else
                        Console.WriteLine("The process cancelled!");

                    break;
                case 'E':

                    storeId = Core.ReadInt("Store Id:");

                    if (Stores.Any(a => a.Id == storeId))
                    {
                        rec = Stores.Where(a => a.Id == storeId).FirstOrDefault();

                        Console.WriteLine("Store is;");
                        Core.Print(rec);

                        rec.Name = Core.ReadStr("Please input new name:");

                        Stores.Remove(rec);
                        Stores.Add(rec);
                    }
                    else
                    {
                        Console.WriteLine("The store coudn't find!");
                    }

                    break;
                case 'R':

                    storeId = Core.ReadInt("Store Id:");

                    if (Stores.Any(a => a.Id == storeId))
                    {
                        rec = Stores.Where(a => a.Id == storeId).FirstOrDefault();

                        Stores.Remove(rec);
                        Console.WriteLine("The store deleted!");
                    }

                    break;
                default:
                    Console.WriteLine("Input couldn't recognized.");
                    break;
            }
        }

        private static void SerialDetail()
        {
            Console.WriteLine("Serial Add/Edit/Remove;");

            Console.WriteLine("Plese select what do you want to do: A,E,R");
            char input = Char.ToUpper(Console.ReadKey().KeyChar);

            Serial rec = new Serial();
            int serialId = 0;
            double price = 0.0;
            string priceStr = "";

            switch (input)
            {
                case 'A':

                    rec.Id = Serials.Select(a => a.Id).OrderByDescending(o => o).FirstOrDefault() + 1;

                    Console.WriteLine("New serial ID is: {0}", rec.Id);
                    rec.Title = Core.ReadStr("Serial title:");
                    rec.Publisher = Core.ReadStr("Publisher:");
                    rec.PlaceOfPublication = Core.ReadStr("Serial place of publication:");
                    rec.Price = Core.ReadDouble("Serial price:");

                    bool editionsAdded = false;
                    List<DateTime> editions = new List<DateTime>();
                    Console.WriteLine("Please input edition of serial(Input format:mm/yyyy");
                    do
                    {
                        string editionStr = Core.ReadStr("Edition date:");
                        DateTime edition = DateTime.Now;

                        if (!DateTime.TryParseExact(editionStr, "MM/yyyy", CultureInfo.InvariantCulture,
                       DateTimeStyles.None, out edition))
                        {
                            Console.WriteLine("\nDatetime not suitable!\n");
                        }
                        else
                        {
                            editions.Add(edition);
                        }

                        Console.WriteLine("Do you want add one more edition? (Y/N)");
                        char editionProcessInput = Char.ToUpper(Console.ReadKey().KeyChar);
                        if (editionProcessInput == 'N')
                            editionsAdded = true;

                    } while (!editionsAdded);
                    rec.Editions = editions;

                    Console.WriteLine("The new serial is: {0}", rec.ToString().Replace("@", System.Environment.NewLine));
                    Console.WriteLine("Are you confirm to create new store? (Y/N)");
                    char confirmChr = Char.ToUpper(Console.ReadKey().KeyChar);

                    if (confirmChr == 'Y')
                        Serials.Add(rec);
                    else
                        Console.WriteLine("The process cancelled!");

                    break;
                case 'E':
                    serialId = Core.ReadInt("Serial Id:");

                    if (Serials.Any(a => a.Id == serialId))
                    {
                        rec = Serials.Where(a => a.Id == serialId).FirstOrDefault();

                        Console.WriteLine("Serial is: {0} \n", rec.ToString().Replace("@", System.Environment.NewLine));

                        Console.Write("Title:");
                        SendKeys.SendWait(rec.Title);
                        rec.Title = Console.ReadLine().Trim();

                        Console.Write("Publisher:");
                        SendKeys.SendWait(rec.Publisher);
                        rec.Publisher = Console.ReadLine().Trim();

                        Console.Write("Place Of Publicetion:");
                        SendKeys.SendWait(rec.PlaceOfPublication);
                        rec.PlaceOfPublication = Console.ReadLine().Trim();


                        priceStr = rec.Price.ToString();
                        Console.Write("Price:");
                        SendKeys.SendWait(priceStr);
                        priceStr = Console.ReadLine().Trim();

                        if (!double.TryParse(priceStr, out price) || price <= 0)
                        {
                            Console.WriteLine("\nSerial price not suitable!\n");
                        }
                        else
                        {
                            rec.Price = price;
                        }

                        string editionListStr = String.Join(",", rec.Editions.Select(a => a.Date.ToString("MM/yyyy")).ToArray());
                        Console.Write("Editions(uses ',' character for add):");
                        SendKeys.SendWait(editionListStr);
                        editionListStr = Console.ReadLine().Trim();

                        List<string> editionsStrList = new List<string>();
                        editionsStrList = editionListStr.Split(',').ToList();

                        rec.Editions = new List<DateTime>();
                        DateTime edition = DateTime.Now;

                        foreach (var item in editionsStrList)
                        {
                            if (DateTime.TryParseExact(item, "MM/yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out edition))
                            {
                                rec.Editions.Add(edition);
                            }
                        }

                        Serials.Remove(rec);
                        Serials.Add(rec);
                    }
                    else
                    {
                        Console.WriteLine("The serial coudn't find!");
                    }

                    break;
                case 'R':
                    serialId = Core.ReadInt("Serial Id:");

                    if (Serials.Any(a => a.Id == serialId))
                    {
                        rec = Serials.Where(a => a.Id == serialId).FirstOrDefault();

                        Serials.Remove(rec);
                        var stocks = Stocks.Where(a => a.Serial.Id == rec.Id).ToList();

                        foreach (var item in stocks)
                        {
                            Stocks.Remove(item);
                        }

                        Console.WriteLine("The serial deleted!");
                    }

                    break;
                default:
                    Console.WriteLine("Input couldn't recognized.");
                    break;
            }
        }

        private static void EditStoreStock()
        {
            Console.WriteLine("Edit Store Stock;");

            BookStore store = new BookStore();
            Serial serial = new Serial();
            int storeId = 0, serialId = 0;

            storeId = Core.ReadInt("Store Id:");
            serialId = Core.ReadInt("Serial Id:");

            if (Serials.Any(a => a.Id == serialId))
                serial = Serials.Where(a => a.Id == serialId).FirstOrDefault();
            else
                Console.WriteLine("The serial coudn't find!");

            if (Stores.Any(a => a.Id == storeId))
            {
                store = Stores.Where(a => a.Id == storeId).FirstOrDefault();
                var stocks = Stocks.Where(a => a.BookStoreId == store.Id && a.Serial.Id == serialId).ToList();

                if (stocks.Count <= 0)
                    Console.WriteLine("This store dosen't have any stock of that serial!");
                else
                {
                    Console.WriteLine("Store stock for this serial;");

                    foreach (var item in stocks)
                    {
                        Stocks.Remove(item);
                        Console.WriteLine("Edition: {1}, Amount:{0}, Markup Percentage:{2}", item.AmountOfCopy, item.Edition.ToString("MM/yyyy"), item.MarkupPercentage);
                    }
                }

                Console.WriteLine("\nAll editions of serial");
                foreach (var item in serial.Editions)
                {
                    Console.WriteLine("Edition: {0}", item.Date.ToString("MM/yyyy"));
                }

                Console.WriteLine("If you want decrese stock; write edition date, minus amount and markup percentage. Example:09/2019,-200,20");
                Console.WriteLine("If you want increse stock or add new edition to stock; write edition date, amount and markup percentage. Example:09/2019,200,20");

                bool stockEditCompleted = false;
                string entry = "";
                List<string> stockEditList = new List<string>();
                DateTime edition = DateTime.Now;
                string stockEditionStr = "";

                do
                {
                    BookStoreSerial stock = new BookStoreSerial();

                    entry = Console.ReadLine();
                    stockEditList = entry.Split(',').ToList();

                    edition = DateTime.Now;
                    stockEditionStr = stockEditList[0] == null || stockEditList[0] == String.Empty ? "" : stockEditList[0];
                    if (!DateTime.TryParseExact(stockEditionStr, "MM/yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out edition))
                    {
                        Console.WriteLine("\n Entry not suitable!");
                        continue;
                    }

                    stock.Edition = edition;
                    stock.AmountOfCopy = stockEditList[1] == null ? 0 : Convert.ToInt32(stockEditList[1]);
                    stock.MarkupPercentage = stockEditList[2] == null ? 0 : Convert.ToInt32(stockEditList[2]);

                    if (stocks.Any(a => a.Edition == stock.Edition))
                    {
                        var oldStock = stocks.Where(a => a.Edition == stock.Edition).FirstOrDefault();

                        stock.AmountOfCopy = oldStock.AmountOfCopy + stock.AmountOfCopy;
                        stock.BookStoreId = oldStock.BookStoreId;
                        stock.Serial = serial;

                        stocks.Remove(oldStock);

                        if (stock.AmountOfCopy > 0)
                            stocks.Add(stock);
                    }
                    else
                    {
                        stock.BookStoreId = storeId;
                        stock.Serial = serial;
                        stocks.Add(stock);
                    }

                    Console.WriteLine("\nCurrent Stock;");
                    foreach (var item in stocks)
                    {
                        Console.WriteLine("Edition: {1}, Amount:{0}, Markup Percentage:{2}", item.AmountOfCopy, item.Edition.Date.ToString("MM/yyyy"), item.MarkupPercentage);
                    }

                    Console.WriteLine("\nDo you want continue editing? (Y/N)");
                    char editionProcessInput = Char.ToUpper(Console.ReadKey().KeyChar);
                    if (editionProcessInput == 'N')
                        stockEditCompleted = true;

                    Console.WriteLine();

                } while (!stockEditCompleted);

                foreach (var item in stocks)
                {
                    Stocks.Add(item);
                }
            }
            else
            {
                Console.WriteLine("The store coudn't find!");
            }
        }

        private static void PrintLowestSerialStock()
        {
            Console.WriteLine("Lowest Serial Stock Of Store;");

            int storeId = Core.ReadInt("Store Id(for search on all stores enter:0):");

            BookStoreSerial stock;

            if (storeId != 0)
            {
                stock = Stocks.Where(a => a.BookStoreId == storeId).OrderBy(o => o.AmountOfCopy).FirstOrDefault();
            }
            else
            {
                stock = Stocks.OrderBy(o => o.AmountOfCopy).FirstOrDefault();
            }

            if (stock != null)
                Core.Print(stock);
            else
                Console.WriteLine("Lowest stock coudn't find!");

        }
    }
}
