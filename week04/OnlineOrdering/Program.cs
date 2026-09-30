using System;
using System.Text;
using System.Collections.Generic;

public class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }

    public double GetTotalCost()
    {
        return _price * _quantity;
    }
}

public class Address
{
    private string _street;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string street, string city, string stateProvince, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country.ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
    }
}

public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName()
    {
        return _name;
    }

    public Address GetAddress()
    {
        return _address;
    }

    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalPrice()
    {
        double total = 0;

        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }

        if (_customer.LivesInUSA())
        {
            total += 5;
        }
        else
        {
            total += 35;
        }

        return total;
    }

    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();

        foreach (Product product in _products)
        {
            label.AppendLine($"{product.GetName()} - ID: {product.GetProductId()}");
        }

        return label.ToString();
    }

    public string GetShippingLabel()
    {
        return $"{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address(
            "123 Main Street",
            "New York",
            "NY",
            "USA"
        );

        Customer customer1 = new Customer(
            "John Smith",
            address1
        );

        Product product1 = new Product(
            "Wireless Mouse",
            "WM001",
            25.99,
            2
        );

        Product product2 = new Product(
            "Mechanical Keyboard",
            "MK002",
            79.99,
            1
        );

        Product product3 = new Product(
            "USB-C Cable",
            "UC003",
            12.50,
            3
        );

        Order order1 = new Order(customer1);

        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Address address2 = new Address(
            "45 Avenue Lumumba",
            "Lubumbashi",
            "Haut-Katanga",
            "DR Congo"
        );

        Customer customer2 = new Customer(
            "Levi Tshipamba",
            address2
        );

        Product product4 = new Product(
            "Laptop Stand",
            "LS004",
            35.50,
            1
        );

        Product product5 = new Product(
            "Webcam",
            "WC005",
            49.99,
            2
        );

        Product product6 = new Product(
            "Headphones",
            "HP006",
            59.99,
            1
        );

        Order order2 = new Order(customer2);

        order2.AddProduct(product4);
        order2.AddProduct(product5);
        order2.AddProduct(product6);

        Console.WriteLine("=================================");
        Console.WriteLine("           ORDER 1");
        Console.WriteLine("=================================");
        Console.WriteLine();

        Console.WriteLine("PACKING LABEL:");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(order1.GetPackingLabel());

        Console.WriteLine("SHIPPING LABEL:");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(order1.GetShippingLabel());

        Console.WriteLine();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"TOTAL PRICE: ${order1.CalculateTotalPrice():F2}");
        Console.WriteLine("=================================");

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("=================================");
        Console.WriteLine("           ORDER 2");
        Console.WriteLine("=================================");
        Console.WriteLine();

        Console.WriteLine("PACKING LABEL:");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(order2.GetPackingLabel());

        Console.WriteLine("SHIPPING LABEL:");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(order2.GetShippingLabel());

        Console.WriteLine();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"TOTAL PRICE: ${order2.CalculateTotalPrice():F2}");
        Console.WriteLine("=================================");
    }
}