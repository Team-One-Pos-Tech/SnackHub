using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities;

public class Order : Entity<Guid>, IAggregateRoot
{
    public virtual Guid ClientId { get; init; }
    public virtual IReadOnlyCollection<OrderItem> Items { get; }
    public virtual OrderStatus Status { get; private set; }
    public decimal Total => Items.Sum(o => o.Total);

    protected Order() : base(Guid.NewGuid())
    {
        Items = [];
    }
    
    protected Order(Guid clientId, IReadOnlyCollection<OrderItem> items) 
        : base(Guid.NewGuid())
    {
        ArgumentOutOfRangeException.ThrowIfEqual(clientId, Guid.Empty);
        ArgumentNullException.ThrowIfNull(items);
        
        ClientId = clientId;
        Items = items;
        Status = OrderStatus.Pending;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new DomainException($"Order cannot be confirmed. Current status: {Status}");
        }

        if (Items.Count == 0)
        {
            throw new DomainException("Order must have at least one item to be confirmed");
        }
        
        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (new [] { OrderStatus.Cancelled, OrderStatus.Accepted, OrderStatus.Declined }.Contains(Status))
        {
            throw new DomainException($"Order is already {Enum.GetName(Status)?.ToLowerInvariant()} and cannot be cancelled");
        }
        
        Status = OrderStatus.Cancelled;
    }
    
    public static class Factory
    {
        public static Order Create(Guid clientId, IReadOnlyCollection<OrderItem> items)
        {
            return new Order(clientId, items);
        }
    }
}
