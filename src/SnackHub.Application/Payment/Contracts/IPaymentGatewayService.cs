using SnackHub.Application.Payment.Models;

namespace SnackHub.Application.Payment.Contracts;

public interface IPaymentGatewayService
{
    Task<PaymentResponse> Process(PaymentRequest request);
}