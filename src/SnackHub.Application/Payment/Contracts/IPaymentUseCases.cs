using SnackHub.Application.Payment.Models;

namespace SnackHub.Application.Payment.Contracts
{
    public interface IPaymentUseCases
    {
        Task<ManagePaymentResponse> UpdatePaymentStatusAsync(PaymentStatusDto paymentStatus);
    }
}
