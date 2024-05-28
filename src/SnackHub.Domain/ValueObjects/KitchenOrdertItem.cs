namespace SnackHub.Domain.ValueObjects;

public class KitchenOrdertItem : ValueObject
{
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    
    protected KitchenOrdertItem(string productName, int quantity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        
        ProductName = productName;
        Quantity = quantity;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductName;
        yield return Quantity;
    }
    
    public static class Factory
    {
        public static KitchenOrdertItem Create(string productName, int quantity)
        {
            return new KitchenOrdertItem(productName, quantity);
        }
    }
}