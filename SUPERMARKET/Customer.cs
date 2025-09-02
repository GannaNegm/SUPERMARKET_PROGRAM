using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Customer
    {
        public string name;  // Customer name
        public List<Product> cart;  // Customer Cart List


        public Customer()  //Constructor
        {
            cart = new List<Product>();
        }

        public void BuyProduct(Product p, int quantity) // Customer's Buy Product Method
        {
            // checking available quantity.
            if (p.Quantity >= quantity)
            {
                p.Quantity -= quantity;

                Product existingInCart = null;

                // if item exist in Customer's cart 
                foreach (Product item in cart)
                {
                    if (item.Name.ToLower() == p.Name.ToLower())
                    {
                        existingInCart = item;
                        break;
                    }
                }

                if (existingInCart != null)
                {
                    existingInCart.Quantity += quantity;
                }
                else
                {
                    Product cartItem = new Product
                    {
                        Name = p.Name,
                        Category = p.Category,
                        Quantity = quantity,
                        ProductionDate = p.ProductionDate,
                        ExpiryDate = p.ExpiryDate
                    };
                    cart.Add(cartItem);
                }

                Console.WriteLine($"Successfully bought {quantity} {p.Name}(s)");
            }
            else
            {
                Console.WriteLine($"Out of stock, only {p.Quantity} {p.Name}(s) available");
            }
        }
        // View customer's cart method
        public void viewCart()
        {
            Console.WriteLine($"\n=== {name}'s Cart ===");

            // Check if there any product exist
            if (cart.Count == 0)
            {
                Console.WriteLine($"Cart is empty.");
            }
            else
            {
                // Viewing Cart 
                foreach (Product item in cart)
                {
                    Console.WriteLine($" - {item.Name} (Qty: {item.Quantity})");
                }
            }
        }
    }
}
