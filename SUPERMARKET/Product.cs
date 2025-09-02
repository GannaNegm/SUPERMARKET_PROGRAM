using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Quantity { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        //Method to display product details
        public void Display()
        {
            Console.WriteLine($"Product: {Name}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Quantity: {Quantity}");
            Console.WriteLine($"Production Date: {ProductionDate.ToShortDateString()}");
            Console.WriteLine($"Expiry Date: {ExpiryDate.ToShortDateString()}");
        }
    }
}
