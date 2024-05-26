namespace SnackHub.Application.Payment.Models;

public record PaymentResponse(string TransactionId, PaymentStatus Status);