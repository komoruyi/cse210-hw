using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineOrdering
{
    // Address class
    public class Address
    {
        private string street;
        private string city;
        private string stateOrProvince;
        private string country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            this.street = street;
            this.city = city;
            this.stateOrProvince = stateOrProvince;
            this.country = country;
        }

        public bool IsInUSA()
        {
            return country.Trim().ToUpper() == "USA";
        }

        public string GetAddressString()
        {
            return $"{street}\n{city}, {stateOrProvince}\n{country}";
        }
    }

    // Customer class
    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public string GetName()
        {
            return name;
        }

        public Address GetAddress()
        {
            return address;
        }

        public bool IsInUSA()
        {
            return address.IsInUSA();
        }
    }

    // Product class
    public class Product
    {
        private string name;
        private string productId;
        private double price;
        private int quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        public string GetName()
        {
            return name;
        }

        public string GetProductId()
        {
            return productId;
        }

        public double GetPrice()
        {
            return price;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public double GetTotalCost()
        {
            return price * quantity;
        }
    }

    // Order class
    public class Order
    {
        private List<Product> products = new List<Product>();
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public double GetTotalPrice()
        {
            double total = 0;
            foreach (var product in products)
            {
                total += product.GetTotalCost();
            }
            total += customer.IsInUSA() ? 5.0 : 35.0;
            return total;
        }

        public string GetPackingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packing Label:");
            foreach (var product in products)
            {
                sb.AppendLine($"{product.GetName()} ({product.GetProductId()})");
            }
            return sb.ToString();
        }

        public string GetShippingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Shipping Label:");
            sb.AppendLine(customer.GetName());
            sb.AppendLine(customer.GetAddress().GetAddressString());
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create customers
            Address addr1 = new Address("123 Main St", "Springfield", "IL", "USA");
            Customer cust1 = new Customer("John Doe", addr1);

            Address addr2 = new Address("456 Queen St", "Toronto", "ON", "Canada");
            Customer cust2 = new Customer("Jane Smith", addr2);

            // Create products
            Product prod1 = new Product("Laptop", "A100", 800.00, 1);
            Product prod2 = new Product("Mouse", "B200", 25.00, 2);
            Product prod3 = new Product("Keyboard", "C300", 45.00, 1);

            Product prod4 = new Product("Book", "D400", 15.00, 3);
            Product prod5 = new Product("Pen", "E500", 2.00, 5);

            // Create orders
            Order order1 = new Order(cust1);
            order1.AddProduct(prod1);
            order1.AddProduct(prod2);
            order1.AddProduct(prod3);

            Order order2 = new Order(cust2);
            order2.AddProduct(prod4);
            order2.AddProduct(prod5);

            // Store orders in a list
            List<Order> orders = new List<Order> { order1, order2 };

            // Display results
            int orderNum = 1;
            foreach (var order in orders)
            {
                Console.WriteLine($"Order {orderNum}:");
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Price: ${order.GetTotalPrice():F2}");
                Console.WriteLine(new string('-', 40));
                orderNum++;
            }
        }
    }
}