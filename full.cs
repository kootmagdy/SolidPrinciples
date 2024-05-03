public class ECommerceSystem
{
    private List<Product> products = new List<Product>();
    private List<Order> orders = new List<Order>();
    public void AddProduct(string name, decimal price, int quantity)
    {
        products.Add(new Product
        {
            Name = name,
            Price = price,
            Quantity =
        quantity
        });
    }

    private void ProcessCreditCardPayment(decimal amount)
    {
        // Process credit card payment
        Console.WriteLine($"Processing credit card payment of ${amount}");
    }
    private void ProcessPayPalPayment(decimal amount)
    {
        // Process PayPal payment
        Console.WriteLine($"Processing PayPal payment of ${amount}");
    }

    public void PlaceOrder(string customerName, List<int> productIds, string
    paymentMethod)
    {
        decimal totalCost = 0;
        List<Product> orderedProducts = new List<Product>();
        foreach (int productId in productIds)
        {
            Product product = products.Find(p => p.Id == productId);
            if (product != null && product.Quantity > 0)
            {
                orderedProducts.Add(product);
                totalCost += product.Price;
                product.Quantity--;
            }
        }
        if (orderedProducts.Count > 0)
        {
            if (paymentMethod == "CreditCard")
            {
                ProcessCreditCardPayment(totalCost);
            }
            else if (paymentMethod == "PayPal")
            {
                ProcessPayPalPayment(totalCost);
            }
            Order order = new Order
            {
                CustomerName = customerName,
                Products = orderedProducts,
                TotalCost = totalCost
            };
            orders.Add(order);
            SendOrderConfirmationEmail(order);
        }
    }
    private void SendOrderConfirmationEmail(Order order)
    {
        string message = $"Order confirmation for {order.CustomerName}:\n";
        message += $"Total Cost: ${order.TotalCost}\n";
        message += "Products:\n";
        foreach (Product product in order.Products)
        {
            message += $"- {product.Name} (${product.Price})\n";
        }
        // Send email
        Console.WriteLine(message);
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class Order
{
    public string CustomerName { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalCost { get; set; }
}