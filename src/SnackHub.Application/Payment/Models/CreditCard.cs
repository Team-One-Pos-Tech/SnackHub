namespace SnackHub.Application.Payment.Models;

public record CreditCard : PaymentMethod
{
    public required string CardNumber { get; init; }
    public required string CardHolderName { get; init; }
    public required string ExpirationDate { get; init; }
    public required string SecurityCode { get; init; }
}