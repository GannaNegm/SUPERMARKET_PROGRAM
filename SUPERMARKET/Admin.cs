using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Admin(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        // Admin's Check in "User & Pass"
        public bool Login(string inputUsername, string inputPassword)
        {
            return this.Username == inputUsername && this.Password == inputPassword;
        }

        public void addProduct(Product p, double quantity)  // Admin's Add Product Method 
        {
            Product existingProduct = null;

            foreach (Product prod in Program.products)
            {
                if (prod.Name.ToLower() == p.Name.ToLower()) // Compare by lower case
                {
                    existingProduct = prod;
                    break; // Break when reaching product name in products list
                }
            }

            if (existingProduct != null)
            {
                // Product exists then => update quantity
                existingProduct.Quantity += quantity;
                Console.WriteLine($"Updated {p.Name} quantity to {existingProduct.Quantity}");
            }
            else
            {
                // Product doesn't exist then => add it
                Program.products.Add(p);
                Console.WriteLine($"Added new product: {p.Name}");
            }
        }

        public static void ViewProducts()  // View Products Method 
        {
            Console.WriteLine("\n=== ALL PRODUCTS ===");
            foreach (Product p in Program.products)
            {
                p.Display();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new string('-', 30));
                Console.ResetColor();
            }
        }
    }
}
