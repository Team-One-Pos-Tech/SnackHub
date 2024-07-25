using Flunt.Notifications;
using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.Payment.UseCases
{
    public class UpdatePaymentStatusUseCase : IPaymentUseCases
    {
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePaymentStatusUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<ManagePaymentResponse> UpdatePaymentStatusAsync(PaymentStatusDto paymentStatus)
        {
            var response = new ManagePaymentResponse();

            // Lógica para atualizar o status do pagamento no banco de dados
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentStatus.PaymentId);
            if (payment == null)
            {
                response.AddNotification(new Notification("Payment", "Payment not found"));
                return response;
            }

            payment.UpdateStatus(paymentStatus.NewStatus);
            await _paymentRepository.UpdateAsync(payment);

            response.IsValid = true;
            return response;
        }
    }
}
