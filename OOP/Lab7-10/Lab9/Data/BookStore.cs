using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Data
{
    public class BookStore : IPrintable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return @"@Id: " + this.Id.ToString()
                + "@Name: " + this.Name
                + "@";
        }
    }
}
