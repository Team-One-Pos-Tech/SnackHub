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
        Status = OrderStatus.Pending;
    }
    
    protected Order(Guid clientId, IReadOnlyCollection<OrderItem> items) 
        : this()
    {
        ArgumentOutOfRangeException.ThrowIfEqual(clientId, Guid.Empty);
        ArgumentNullException.ThrowIfNull(items);
        
        ClientId = clientId;
        Items = items;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new DomainException($"Order is {GetStatusDescription()} and cannot be confirmed at this time");
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
            throw new DomainException($"Order is already {GetStatusDescription()} and cannot be cancelled at this time");
        }
        
        Status = OrderStatus.Cancelled;
    }
    
    public void Checkout(bool accepted)
    {
        if (Status != OrderStatus.Confirmed)
        {
            throw new DomainException($"Order is {GetStatusDescription()} and cannot be checkout out at this time");
        }
        
        Status = accepted ? OrderStatus.Accepted : OrderStatus.Declined;
    }
    
    private string? GetStatusDescription()
    {
        return Enum.GetName(Status)?.ToLowerInvariant();
    }
    
    public static class Factory
    {
        public static Order Create(Guid clientId, IReadOnlyCollection<OrderItem> items)
        {
            return new Order(clientId, items);
        }
    }
}
