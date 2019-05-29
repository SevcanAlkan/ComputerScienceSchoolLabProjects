using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public static class Core
    {
        public static void Print<T>(T rec)
        {
            if (rec is IPrintable && rec != null)
                Console.Write(rec.ToString().Replace("@", System.Environment.NewLine));

        }
        public static void PrintList<T>(List<T> collection)
        {
            if (collection != null)
                foreach (var item in collection)
                {
                    if (item is IPrintable)
                        Console.Write(item.ToString().Replace("@", System.Environment.NewLine));
                }
        }

        public static int StrToInt(string value)
        {
            int result = 0;

            if (!int.TryParse(value, out result))
            {
                Console.WriteLine("\nInput value not suitable!\n");
            }

            return result;
        }
        public static double StrToDouble(string value)
        {
            double result = 0;

            if (!double.TryParse(value, out result))
            {
                Console.WriteLine("\nInput value not suitable!\n");
            }

            return result;
        }

        public static int ReadInt(string text)
        {
            Console.Write(text);
            string numberStr = Console.ReadLine();
            numberStr = numberStr.Trim();
            return StrToInt(numberStr);
        }
        public static double ReadDouble(string text)
        {
            Console.Write(text);
            string inputStr = Console.ReadLine();
            inputStr = inputStr.Trim();
            return StrToDouble(inputStr);
        }
        public static string ReadStr(string text)
        {
            Console.Write(text);
            return Console.ReadLine().Trim();
        }


    }
}
