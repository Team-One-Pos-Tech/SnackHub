using Microsoft.Extensions.Logging;
using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Models;

namespace SnackHub.Application.Payment.Services;

public class FakePaymentGatewayService : IPaymentGatewayService
{
    private readonly ILogger<FakePaymentGatewayService> _logger;

    public FakePaymentGatewayService(ILogger<FakePaymentGatewayService> logger)
    {
        _logger = logger;
    }
    
    public async Task<PaymentResponse> Process(PaymentRequest request)
    {
        _logger.LogInformation("Processing payment request {@Request}", request);
        
        // Simulate payment processing delay
        await Task.Delay(1000);
        
        return new PaymentResponse(Guid.NewGuid().ToString("N"), PaymentStatus.Success);
    }
}