namespace SnackHub.Application.Order.Models;

public class ConfirmOrderRequest
{
    public required Guid ClientId { get; init; }
    public required IEnumerable<Item> Items { get; init; } = [];

    public record Item(Guid ProductId, int Quantity);
}