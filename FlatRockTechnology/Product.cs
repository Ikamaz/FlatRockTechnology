using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRockTechnology
{
    public class Product
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public float Rating { get; set; }

        public override string ToString() => $"ProductName {ProductName}\nPrice {Price}\nRating {Rating}\n";
    }
}
