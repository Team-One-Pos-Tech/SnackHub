namespace SnackHub.Application.KitchenRequest.Models;

public class UpdateKitchenRequestStatusRequest
{
    public required Guid OrderId { get; init; }
}