namespace SnackHub.Application.KitchenOrder.Models;

public class UpdateKitchenOrderStatusRequest
{
    public required Guid OrderId { get; init; }
}