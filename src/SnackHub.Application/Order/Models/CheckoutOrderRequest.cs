using SnackHub.Application.Payment.Models;

namespace SnackHub.Application.Order.Models;

public class CheckoutOrderRequest
{
    public required Guid OrderId { get; init; }
    
    // TODO: make this public an expose it on swagger as OneOf, listing all subclasses  
    internal PaymentMethod PaymentMethod { get; init; } = new OnTheHouse();
}