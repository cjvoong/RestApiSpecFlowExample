using Newtonsoft.Json;

public class Order
{
    [JsonProperty]
    private List<OrderItem> orderItems;

    public Order()
    {
        orderItems = new List<OrderItem>();
    }

    public void AddOrderItem(OrderItem item)
    {
        orderItems.Add(item);
    }

    public double GetTotalCost()
    {
        double totalCost = 0;
        foreach (var item in orderItems)
        {
            totalCost += item.GetItemCost();
        }
        return totalCost;
    }

    public string GetOrderSummary()
    {
        string summary = "";
        foreach (var item in orderItems)
        {
            summary += $"{item}\n";
        }
        return summary;
    }
}