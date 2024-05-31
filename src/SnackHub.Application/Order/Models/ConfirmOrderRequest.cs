namespace SnackHub.Application.Order.Models;

public class ConfirmOrderRequest
{
    public required string Identifier { get; init; }
    public required IEnumerable<Item> Items { get; init; } = [];

    public record Item(Guid ProductId, int Quantity);
}