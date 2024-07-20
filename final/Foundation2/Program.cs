using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public string ProductID { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, string productID, double price, int quantity)
    {
        Name = name;
        ProductID = productID;
        Price = price;
        Quantity = quantity;
    }

    public double GetTotalCost()
    {
        return Price * Quantity;
    }
}

class Address
{
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public Address(string streetAddress, string city, string state, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        Country = country;
    }

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public override string ToString()
    {
        return $"{StreetAddress}\n{City}, {State}\n{Country}";
    }
}

class Customer
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

class Order
{
    private List<Product> products;
    public Customer Customer { get; set; }

    public Order(Customer customer)
    {
        Customer = customer;
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalCost()
    {
        double totalCost = 0;
        foreach (var product in products)
        {
            totalCost += product.GetTotalCost();
        }
        totalCost += Customer.IsInUSA() ? 5 : 35;
        return totalCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in products)
        {
            label += $"{product.Name} (ID: {product.ProductID})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.Name}\n{Customer.Address}";
    }
}

class Program
{
    static void Main()
    {
        // Create products
        Product product1 = new Product("Product 1", "P001", 10.0, 2);
        Product product2 = new Product("Product 2", "P002", 20.0, 1);
        Product product3 = new Product("Product 3", "P003", 15.0, 3);
        Product product4 = new Product("Product 4", "P004", 30.0, 2);

        // Create addresses
        Address address1 = new Address("123 Main St", "CityA", "StateA", "USA");
        Address address2 = new Address("456 Another St", "CityB", "StateB", "Canada");

        // Create customers
        Customer customer1 = new Customer("Customer 1", address1);
        Customer customer2 = new Customer("Customer 2", address2);

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product3);
        order2.AddProduct(product4);

        // Display order details
        List<Order> orders = new List<Order> { order1, order2 };

        foreach (var order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.GetTotalCost()}");
            Console.WriteLine();
        }
    }
}