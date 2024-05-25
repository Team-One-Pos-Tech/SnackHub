using SnackHub.Application.Payment.Models;

namespace SnackHub.Application.Order.Models;

public class CheckoutOrderRequest
{
    public required Guid OrderId { get; init; }
    internal PaymentMethod PaymentMethod { get; init; } = new OnTheHouse();
}