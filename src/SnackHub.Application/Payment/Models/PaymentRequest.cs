namespace SnackHub.Application.Payment.Models;

public record PaymentRequest(
    decimal Amount, 
    PaymentMethod PaymentMethod, 
    object? Metadata = null
);