using System;
using System.Collections.Generic;
using System.Linq; // Klicove pro LINQ

// vypnutí napovídání v settings: user settings (JSON)
// "editor.inlineSuggest.enabled": false

public class LinqExerciseData
{
    // Definice trid pro testovaci data
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return $"Product: {Name} ({Category}), Price: {Price:C}, Manufacturer: {Manufacturer}, Available: {IsAvailable}";
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Customer: {Name} ({City}), Email: {Email}";
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } // Odkazuje na Customer.Id
        public int ProductId { get; set; }  // Odkazuje na Product.Id
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        public override string ToString()
        {
            return $"Order ID: {OrderId}, Customer ID: {CustomerId}, Product ID: {ProductId}, Quantity: {Quantity}, Date: {OrderDate:d}";
        }
    }

    public static void Main(string[] args)
    {
        // Testovaci data
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Pro", Category = "Electronics", Price = 25000.00m, Manufacturer = "TechCorp", IsAvailable = true },
            new Product { Id = 2, Name = "Gaming Mouse", Category = "Electronics", Price = 850.00m, Manufacturer = "GameGear", IsAvailable = true },
            new Product { Id = 3, Name = "Ergonomic Keyboard", Category = "Electronics", Price = 1200.00m, Manufacturer = "ErgoTech", IsAvailable = false },
            new Product { Id = 4, Name = "Office Chair", Category = "Furniture", Price = 3500.00m, Manufacturer = "ComfortHome", IsAvailable = true },
            new Product { Id = 5, Name = "Desk Lamp", Category = "Lighting", Price = 700.00m, Manufacturer = "BrightLight", IsAvailable = true },
            new Product { Id = 6, Name = "External SSD 1TB", Category = "Storage", Price = 2800.00m, Manufacturer = "FastData", IsAvailable = true },
            new Product { Id = 7, Name = "Monitor 27-inch", Category = "Electronics", Price = 7800.00m, Manufacturer = "ViewSharp", IsAvailable = true },
            new Product { Id = 8, Name = "Webcam HD", Category = "Electronics", Price = 950.00m, Manufacturer = "StreamCam", IsAvailable = false },
            new Product { Id = 9, Name = "Coffee Machine", Category = "Appliances", Price = 4200.00m, Manufacturer = "BrewMaster", IsAvailable = true },
            new Product { Id = 10, Name = "Bluetooth Speaker", Category = "Audio", Price = 1500.00m, Manufacturer = "SoundBlast", IsAvailable = true }
        };

        List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 101, Name = "Anna Nováková", City = "Praha", Email = "anna.n@example.com" },
            new Customer { Id = 102, Name = "Petr Svoboda", City = "Brno", Email = "petr.s@example.com" },
            new Customer { Id = 103, Name = "Jana Dvořáková", City = "Ostrava", Email = "jana.d@example.com" },
            new Customer { Id = 104, Name = "Karel Veselý", City = "Plzeň", Email = "karel.v@example.com" },
            new Customer { Id = 105, Name = "Eva Adamová", City = "Olomouc", Email = "eva.a@example.com" },
            new Customer { Id = 106, Name = "Adam Procházka", City = "Liberec", Email = "adam.p@example.com" }
        };

        List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerId = 101, ProductId = 1, Quantity = 1, OrderDate = new DateTime(2024, 5, 20) },
            new Order { OrderId = 2, CustomerId = 102, ProductId = 4, Quantity = 2, OrderDate = new DateTime(2024, 5, 21) },
            new Order { OrderId = 3, CustomerId = 101, ProductId = 7, Quantity = 1, OrderDate = new DateTime(2024, 5, 22) },
            new Order { OrderId = 4, CustomerId = 103, ProductId = 2, Quantity = 3, OrderDate = new DateTime(2024, 5, 22) },
            new Order { OrderId = 5, CustomerId = 104, ProductId = 5, Quantity = 1, OrderDate = new DateTime(2024, 5, 23) },
            new Order { OrderId = 6, CustomerId = 102, ProductId = 6, Quantity = 1, OrderDate = new DateTime(2024, 5, 23) },
            new Order { OrderId = 7, CustomerId = 105, ProductId = 9, Quantity = 1, OrderDate = new DateTime(2024, 5, 24) },
            new Order { OrderId = 8, CustomerId = 101, ProductId = 3, Quantity = 1, OrderDate = new DateTime(2024, 5, 24) } // Produkt 3 neni dostupny
        };

        // Zde muzes zacit psat sve LINQ dotazy pro kazdy ukol
        Console.WriteLine("\n--- Reseni ukolu ---");

        // Příklad pro ukol 1 (muzes smazat a nahradit svym resenim)
        Console.WriteLine("\nUkol 1: Elektronika drazsi nez 1000 Kc (nazev a cena)");

        var elektronicsOver1000 = products.Where(p => p.Price > 1000).Select(p => new { p.Name, p.Price });
        foreach (var item in elektronicsOver1000)
        {
            Console.WriteLine($"{item.Name,-20}: {item.Price,15:C}");
        }


        // Zde pokracuj s resenim dalsich ukolu...
        // Ukol 2: Razeni
        Console.WriteLine("\nUkol 2: Razeni");
        var productsSorted = products.OrderBy(p => p.Category)
                                    .ThenByDescending(p => p.Price)
                                    .Select(p => new { p.Name, p.Category, p.Price });
        foreach (var p in productsSorted)
        {
            Console.WriteLine($"{p.Name,-20} {p.Category,-15}: {p.Price,15:C}");
        }

        // Ukol 3: Seskupovani
        Console.WriteLine("\nUkol 3: Seskupovani");
        var productsGroup = products.GroupBy(
                                    p => p.Category,
                                    (key, pr) => new
                                    {
                                        Category = key,
                                        Count = pr.Count(),
                                        AveragePrice = pr.Average(p => p.Price)
                                    }
                                    );

        foreach (var p in productsGroup)
        {
            Console.WriteLine($"{p.Category,-20}: {p.Count}, Av.Price: {p.AveragePrice,15:C}");
        }
        // Ukol 4: Agregace
        Console.WriteLine("\n4: Agregace");
        var productsCout = products.Count;
        Console.WriteLine($"Celkový počet produktů: {productsCout}");
        var productWithMaxPrice = products.Select(p => new { p.Name, p.Price })
                                            .MaxBy(m => m.Price);
        Console.WriteLine($"Nejdražší produkt: {productWithMaxPrice.Name}: {productWithMaxPrice.Price:C}");

        var TotalOrdersPrice = (from order in orders
                                join product in products on order.ProductId equals product.Id
                                select order.Quantity * product.Price)
                                .Sum();
        Console.WriteLine($"Celková hodnota objednávek: {TotalOrdersPrice:C}");


        // Ukol 5: Mnozinove operace
        Console.WriteLine("\n5: Mnozinove operace");
        Console.WriteLine("Kategorie:");
        var categories = products.DistinctBy(p => p.Category)
                        .Select(c => new { c.Category })
                        .OrderBy(c => c.Category);
        foreach (var c in categories)
        {
            Console.WriteLine($"\t{c.Category}");
        }
        // Ukol 6: Prace s textem a cisly
        Console.WriteLine("\nUkol 6: Prace s textem a cisly");
        var customers_AinName_cityEndsWithE = customers
                                .Where(c => c.Name.Contains('A') &&
                                c.City.EndsWith('e'))
                                .Select(c => new { c.Id, c.Name });
        if (customers_AinName_cityEndsWithE.Count() > 0)
        {
            foreach (var c in customers_AinName_cityEndsWithE)
            {
                Console.WriteLine($"ID: {c.Id,-5}, Name: {c.Name,20}");
            }
        }
        else
        {
            Console.WriteLine($"Nebyl nalezen žádný výsledek");
        }

        // Ukol 7: Spojovani
        Console.WriteLine("\nUkol 7: Spojovani");

        var ordersWithCustomer = from order in orders
                                 join customer in customers
                                 on order.CustomerId equals customer.Id
                                 select new { order.OrderId, customer.Name, order.OrderDate };

        Console.WriteLine($"{"Id objednávky ",-20}{"Zákazník",-20}{"Datum objednávky",20}");
        foreach (var oc in ordersWithCustomer)
        {
            Console.WriteLine($"{oc.OrderId,-20}{oc.Name,-20}{oc.OrderDate.ToString("dd.MM.yyyy"),20}");
        }

        var ordersProductsCustomers = from order in orders
                                      join product in products on order.ProductId
                                         equals product.Id
                                      join customer in customers on order.CustomerId
                                         equals customer.Id
                                      select new
                                      {
                                          OrderId = order.OrderId,
                                          CustomerName = customer.Name,
                                          ProductName = product.Name,
                                          TotalPrice = order.Quantity * product.Price
                                      };
        Console.WriteLine($"{"Id obj.",-7} {"Zákazník",-20}{"Produkt",-20}{"Total",20}");
        foreach (var opc in ordersProductsCustomers)
        {
            Console.WriteLine($"{opc.OrderId,-8}{opc.CustomerName,-20}{opc.ProductName,-20}{opc.TotalPrice,20:C}");
        }
        // Ukol 8: Odlozene provedeni
        Console.WriteLine("\nUkol 8: Odlozene provedeni");

        var productOver200 = products.Where(p => p.Price > 200);

        products.Add(new Product { Id = 11, Name = "Tablet", Category = "Electronics", Price = 300.00m, Manufacturer = "Lenovo", IsAvailable = true });
        foreach (var p in productOver200)
        {
            Console.WriteLine($"{p.Name,-20} {p.Price,20:C}");
        }

        products.RemoveAt(products.Count - 1);
        Console.WriteLine("\nToList() ihned:");
        productOver200 = products.Where(p => p.Price > 200).ToList();

        products.Add(new Product { Id = 11, Name = "Tablet", Category = "Electronics", Price = 300.00m, Manufacturer = "Lenovo", IsAvailable = true });
        foreach (var p in productOver200)
        {
            Console.WriteLine($"{p.Name,-20} {p.Price,20:C}");
        }

        Console.WriteLine("\n--- Konec ukolu ---");

    }
}
