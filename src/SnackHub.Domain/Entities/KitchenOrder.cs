using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities;

public class KitchenOrder : Entity<Guid>, IAggregateRoot
{
    protected KitchenOrder( ): base(Guid.NewGuid()) { }
    
    public KitchenOrder(Guid orderId, IReadOnlyCollection<KitchenOrdertItem> items) 
        : this(Guid.NewGuid(), orderId, items, KitchenOrderStatus.Received)
    {
    }
    
    public KitchenOrder(Guid id, Guid orderId, IReadOnlyCollection<KitchenOrdertItem> items, KitchenOrderStatus status)
        : base(id)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(orderId, Guid.Empty);
        ArgumentNullException.ThrowIfNull(items);
        
        OrderId = orderId;
        Items = items;
        Status = status;
    }
    
    public virtual Guid OrderId { get; init; }
    public virtual IReadOnlyCollection<KitchenOrdertItem> Items { get; private set; }
    public virtual KitchenOrderStatus Status { get; private set; } = KitchenOrderStatus.Received;

    public void UpdateStatus()
    {
        Status = Status switch
        {
            KitchenOrderStatus.Received => KitchenOrderStatus.Preparing,
            KitchenOrderStatus.Preparing => KitchenOrderStatus.Finished,
            KitchenOrderStatus.Finished => KitchenOrderStatus.Done,
            _ => Status
        };
    }
    
    public static class Factory
    {
        public static KitchenOrder Create(Guid orderId, IReadOnlyCollection<KitchenOrdertItem> items)
        {
            return new KitchenOrder(orderId, items);
        }
    }
}