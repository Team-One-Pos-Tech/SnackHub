using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SnackHub.Application.Payment.Models;
using SnackHub.Application.Payment.Services;

namespace SnackHub.Application.Tests.Services;

public class FakePaymentGatewayServiceShould
{
    private Mock<ILogger<FakePaymentGatewayService>> _loggerMock;
    private FakePaymentGatewayService _paymentGatewayService;
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<FakePaymentGatewayService>>();
        _paymentGatewayService = new FakePaymentGatewayService(_loggerMock.Object);
    }
    
    [Test]
    public async Task AlwaysProcessSuccessfully()
    {
        var request = new PaymentRequest(10, new OnTheHouse());
        
        var response = await _paymentGatewayService.Process(request);
        
        response
            .TransactionId
            .Should()
            .NotBeEmpty();
        response
            .Status
            .Should()
            .Be(PaymentStatus.Success);
    }
}