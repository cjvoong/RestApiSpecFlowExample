using domain;

namespace domain {
    public class OrderItem(Product product, int quantity)
    {
        public Product Product { get; } = product;
        public int Quantity { get; } = quantity;

        public double GetItemCost()
        {
            return Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Quantity} {Product.Name}(s) - ${GetItemCost():F2}";
        }
    }
}