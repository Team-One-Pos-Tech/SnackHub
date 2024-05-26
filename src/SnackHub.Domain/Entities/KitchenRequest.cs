using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities;

public class KitchenRequest : Entity<Guid>, IAggregateRoot
{
    public virtual Guid OrderId { get; init; }
    public virtual IReadOnlyCollection<KitchenRequestItem> Items { get; }
    public virtual KitchenRequestStatus Status { get; private set; } = KitchenRequestStatus.Received;

    public void UpdateStatus()
    {
        Status = Status switch
        {
            KitchenRequestStatus.Received => KitchenRequestStatus.Preparing,
            KitchenRequestStatus.Preparing => KitchenRequestStatus.Finished,
            KitchenRequestStatus.Finished => KitchenRequestStatus.Done,
            _ => Status
        };
    }
}