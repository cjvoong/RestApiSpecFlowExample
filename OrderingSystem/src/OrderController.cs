using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private static List<Order> Orders = new List<Order>();

    [HttpPost]
    public ActionResult<Order> CreateOrder([FromBody] List<OrderItemRequest> items)
    {
        Console.WriteLine("Received POST");
        if (items == null || items.Count == 0)
        {
            return BadRequest("Invalid order items.");
        }

        Order order = new Order();

        foreach (var item in items)
        {
            Product product = GetProductByName(item.ProductName);
            if (product == null)
            {
                return BadRequest($"Invalid product name: {item.ProductName}");
            }
            order.AddOrderItem(new OrderItem(product, item.Quantity));
        }

        Orders.Add(order);
        Console.WriteLine("Added order, there are " + Orders.Count + " orders.");
        // Return CreatedAtAction with the action name ("GetOrder") and route values (orderId)
        return CreatedAtAction(nameof(GetOrder), new { orderId = Orders.Count - 1 }, order);
    }

    [HttpGet("{orderId}")]
    public ActionResult<Order> GetOrder(int orderId)
    {
        Console.WriteLine("Received GET");
        if (orderId >= 0 && orderId < Orders.Count)
        {
            Console.WriteLine("Found order " + orderId);
            return Ok(JsonConvert.SerializeObject(Orders[orderId]));
        } else {
            return NotFound("Couldn't find order " + orderId);
        }
    }

    private Product? GetProductByName(string name)
    {
        Product[] products = {
            new Product("Apple", 1.0),
            new Product("Banana", 0.75),
            new Product("Orange", 1.25)
        };

        foreach (var product in products)
        {
            if (product.Name.ToLower() == name.ToLower())
            {
                return product;
            }
        }
        return null;
    }
}