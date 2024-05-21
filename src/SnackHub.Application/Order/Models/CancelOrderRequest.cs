namespace SnackHub.Application.Order.Models;

public class CancelOrderRequest
{
    public required Guid OrderId { get; init; }
}
