using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Data
{
    public class BookStoreSerial : IPrintable
    {
        public Serial Serial { get; set; }
        public DateTime Edition { get; set; }

        public int BookStoreId { get; set; }

        public int AmountOfCopy { get; set; }
        public int MarkupPercentage { get; set; }

        public override string ToString()
        {
            //string serialStr = this.Serial.ToString();
            double wholeSalePrice = ((this.MarkupPercentage * (this.Serial.Price / 100)) + this.Serial.Price);

            return @"@Serial " + this.Serial.Title
                + "@Edition: " + this.Edition.ToString("MM:yyyy")
                + "@Amount Of Copy: " + this.AmountOfCopy.ToString()
                + "@Markup Percentage: %" + this.MarkupPercentage.ToString()
                + "@Price Per Item: " + this.Serial.Price.ToString()
                + "@WholePrice: " + (wholeSalePrice * this.AmountOfCopy).ToString()
                + "@Reatil Price: " + wholeSalePrice.ToString()
                + "@";
        }
    }
}
