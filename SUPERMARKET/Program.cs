using SUPERMARKET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// Class with product members and actions
//public class Product
//{
    
//}

// Customer class  => BuyProduct(),  => viewCart()
//public class Customer
//{
    

//}

// Admin class  => addProduct(),  => ViewProducts()
//public class Admin
//{

//}


namespace SUPERMARKET
{
    internal class Program
    {

        // Product list by Product class
        public static List<Product> products = new List<Product>();
        public static Admin admin = new Admin("admin", "1234");   // Admin's user & pass


        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                   === Welcome to Supermarket Management System ===");
            Console.ResetColor();

            loadProductsFromFile();  // Initiate Some Products 

            while (true)
            {
                showMainMenu();  // Main Choice  => Customer, or  => Admin
                string choice = Console.ReadLine(); 

                switch (choice)
                {
                    case "1":
                        CustomerMenu();  // Customer Program Options 
                        break;
                    case "2":
                        Adminlogin();  // Admins Username and Password 
                        break;
                    case "3":
                        Console.WriteLine($"Thank you for using this system.");  //  Before Exitting Prog
                        return;
                    default:
                        Console.WriteLine($"Invalid choice! Please Try again.");
                        break;
                }
            }
        }

        public static void showMainMenu()  // Main Menu
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n=== MAIN MENU ===");
            Console.WriteLine($"1. Customer");
            Console.WriteLine($"2. Admin");
            Console.WriteLine($"3. Exit");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Choose an option: ");
            Console.ResetColor();
        }

        public static void CustomerMenu()  // Customer Menu
        {
            // take and save customer name 
            Console.Write($"Enter your name: ");
            string customerName = Console.ReadLine();
            Customer customer = new Customer { name = customerName };  // Saving Customer Name 

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n=== Welcome {customerName} ===");
                Console.ResetColor();
                Console.WriteLine($"1. View Products.");
                Console.WriteLine($"2. Buy Product.");
                Console.WriteLine($"3. View Cart");
                Console.WriteLine($"4. Back To The Main Menu.");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"Choose an option: ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        viewAllProducts();  // View All Available Products 
                        break;
                    case "2":
                        buyProductMenu(customer);  // Buy Product 
                        break;
                    case "3":
                        customer.viewCart();  // View Customer Cart 
                        break;
                    case "4":
                        return;  // To Back again to the main
                    default:
                        Console.WriteLine($"Invalid choice! Please try again.");
                        break;

                }
            }
        }

        public static void Adminlogin()  // Login strategy
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (admin.Login(username, password))
            {
                AdminMenu();  // Admin's Main Program Options 
            }
            else
            {
                Console.WriteLine("Login failed!");
            }

        }

        public static void AdminMenu()  // Main Menu
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n=== ADMIN PANEL ===");
                Console.ResetColor();
                Console.WriteLine($"1. Add Product.");
                Console.WriteLine($"2. View All Products.");
                Console.WriteLine($"3. View Expiry Alerts");
                Console.WriteLine($"4. Back To Main Menu.");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"Choose an option: ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        addProductMenu();  // Add new product or Update existing product 
                        break;
                    case "2":
                        viewAllProducts();  // View All product With Expiry Alert 
                        break;
                    case "3":
                        viewExpiryAlerts();  // All Expiry Alert
                        break;
                    case "4":
                        return; // Get to the main Menu
                    default:
                        Console.WriteLine($"Invalid choice! Please try again.");
                        break;
                }
            }
        }

        public static void viewAllProducts()  // View All Product List Method 
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n=== AVAILABLE PRODUCTS ===");
            Console.ResetColor();
            // If any product exist 
            if (products.Count == 0)
            {
                Console.WriteLine("No Products available!!!");
                return;
            }

            foreach (Product p in products)
            {
                string alertSymbol;  // Check Expiry befor view any product 
                Console.Write($". ");

                if (isExpired(p))
                {
                    alertSymbol = "[  EXPIRED  ]";
                    Console.Write($"{p.Name} - {p.Category}");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($" => ");
                    Console.WriteLine($"{alertSymbol}");
                    Console.ResetColor();
                    Console.WriteLine($"  Quantity: {p.Quantity}");
                    Console.WriteLine($"  Expires: {p.ProductionDate}");
                    Console.WriteLine($"  Expires: {p.ExpiryDate}");
                    Console.WriteLine();
                }
                else if (checkExpiry(p))
                {
                    alertSymbol = "[NEAR EXPIRY]";
                    Console.Write($"{p.Name} - {p.Category}");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($" => ");
                    Console.WriteLine($"{alertSymbol}");
                    Console.ResetColor();
                    Console.WriteLine($"  Quantity: {p.Quantity}");
                    Console.WriteLine($"  Expires: {p.ProductionDate}");
                    Console.WriteLine($"  Expires: {p.ExpiryDate}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"{p.Name} - {p.Category}");
                    Console.WriteLine($"  Quantity: {p.Quantity}");
                    Console.WriteLine($"  Expires: {p.ProductionDate}");
                    Console.WriteLine($"  Expires: {p.ExpiryDate}");
                    Console.WriteLine();
                }

            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();

        }

        private static void buyProductMenu(Customer customer)  // Customer Buy Product Method 
        {
            viewAllProducts();  // View All befor Buy 
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Enter product name to buy: ");
            Console.ResetColor();
            string productName = Console.ReadLine();

            Product product = null;

            foreach (Product p in products)  // Check if product exist in products list 
            {
                if (p.Name.ToLower() == productName.ToLower())  // Compare By Lower Case 
                {
                    product = p;
                    break; // Break when found 
                }
            }

            if (product != null)
            {
                
                Console.Write($"Enter quantity ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"(available: {product.Quantity}): ");
                Console.ResetColor();
                if (int.TryParse(Console.ReadLine(), out int quantity))  // Get Only numbers + boolian check
                {
                    customer.BuyProduct(product, quantity);  // Remove needed Qty by Customer BuyProduct Method 
                }
                else
                {
                    Console.WriteLine("Invalid quantity!");
                }
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        public static void addProductMenu()  // Admin's Adding Product Method 
        {
            Console.WriteLine("\n=== ADD PRODUCT ===");
            Console.Write("Product name: ");
            string name = Console.ReadLine();

            Console.Write("Category: ");
            string category = Console.ReadLine();

            Console.Write("Quantity: ");
            double quantity = Convert.ToDouble(Console.ReadLine());

            Console.Write("Production Date (YYYY-MM-DD): ");
            DateTime production = DateTime.Parse(Console.ReadLine());

            Console.Write("Expiry Date (YYYY-MM-DD): ");
            DateTime expiry = DateTime.Parse(Console.ReadLine());

            Product newProduct = new Product // Setting All new Product Details 
            {
                Name = name,
                Category = category,
                Quantity = quantity,
                ProductionDate = production,
                ExpiryDate = expiry
            };


            admin.addProduct(newProduct, quantity);  // Add New Product To The List Or Update Qty If Exist 
        }

        public static bool isExpired(Product product) // Checks if product is oready expired
        {
            DateTime today = DateTime.Today;
            DateTime expiry = product.ExpiryDate;
            return expiry < today;
        }

        public static bool checkExpiry(Product product)  // Check expiry method befor Expiry Date within 1:5 days 
        {
            DateTime today = DateTime.Today;
            DateTime expiry = product.ExpiryDate;

            int daysuntillExpiry = (expiry - today).Days; // remaining days (untill expiry)

            return daysuntillExpiry <= 5 && daysuntillExpiry >= 0;
        }

        public static void viewExpiryAlerts() // All expiry Alerts 
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n=== EXPIRY ALERTS ===");
            Console.ResetColor();
            bool producthasalert = false;

            foreach (Product product in products)
            {
                DateTime expiry = product.ExpiryDate;
                int daysleft = (expiry - DateTime.Today).Days;

                if (isExpired(product))
                {
                    producthasalert = true;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Product: {product.Name} => Category: {product.Category}. [  EXPIRED  ] ");
                    Console.WriteLine($"Expired {Math.Abs(daysleft)} days ago ({product.ExpiryDate}).");
                    Console.WriteLine($"Current quantity : {product.Quantity}.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"             " + new string('-', 40));
                    Console.ResetColor();
                }
                else if (checkExpiry(product))
                {
                    producthasalert = true;

                    Console.WriteLine($"Product: {product.Name} => Category: {product.Category}. [NEAR EXPIRY] ");
                    Console.WriteLine($"Expires in {daysleft} days ({product.ExpiryDate}).");
                    Console.WriteLine($"Current quantity : {product.Quantity}.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"             " + new string('-', 40));
                    Console.ResetColor();
                }
            }

            if (!producthasalert)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(":) ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No products near expiry!");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void loadProductsFromFile()  // Initial Main Products
        {
            products.Add(new Product  // First Product 
            {
                Name = "Milk",
                Category = "Dairy",
                Quantity = 20,
                ProductionDate = DateTime.Parse("2025 - 08 - 01"),
                ExpiryDate = DateTime.Parse("2025-12-20")
            });

            products.Add(new Product  // Second Product 
            {
                Name = "Honey",
                Category = "Sugaries",
                Quantity = 10,
                ProductionDate = DateTime.Parse("2025 - 06 - 15"),
                ExpiryDate = DateTime.Parse("2025-8-30")
            });

            products.Add(new Product  // Third Product 
            {
                Name = "Bread",
                Category = "Bakery",
                Quantity = 50,
                ProductionDate = DateTime.Parse("2025 - 08 - 10"),
                ExpiryDate = DateTime.Parse("2025-10-30")
            });

            products.Add(new Product  // Forth Product 
            {
                Name = "yugart",
                Category = "Dairy",
                Quantity = 50,
                ProductionDate = DateTime.Parse("2025 - 08 - 15"),
                ExpiryDate = DateTime.Parse("2025-9-08")
            });

        }
    }
}
