using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Data
{
    public class Serial : IPrintable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string PlaceOfPublication { get; set; }

        public List<DateTime> Editions { get; set; }

        public double Price { get; set; }

        public Serial()
        {
            Editions = new List<DateTime>();
        }

        public override string ToString()
        {
            return @"@Id: " + this.Id.ToString()
                + "@Title: " + this.Title
                + "@Title: " + this.Publisher
                + "@Place Of Publication: " + this.PlaceOfPublication
                + "@Date: " + String.Join(", ", Editions.Select(p => p.ToString("MM:yyyy")).ToArray())
                + "@Price: " + this.Price.ToString()
                + "@";
        }
    }
}
