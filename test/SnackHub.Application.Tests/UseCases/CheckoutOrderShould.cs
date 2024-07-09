using FluentAssertions;
using Moq;
using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.Models;
using SnackHub.Application.Order.Models;
using SnackHub.Application.Order.UseCases;
using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

using OrderFactory = SnackHub.Domain.Entities.Order.Factory;
using OrderItemFactory = SnackHub.Domain.ValueObjects.OrderItem.Factory;

namespace SnackHub.Application.Tests.UseCases;

public class CheckoutOrderShould
{
    private Mock<IPaymentGatewayService> _paymentGatewayMock;
    private Mock<IOrderRepository> _orderRepositoryMock;
    private Mock<ICreateKitchenOrderUseCase> _createKitchenOrderUseCase;
    
    private CheckoutOrderUseCase _checkoutOrderUseCase;
    
    [SetUp]
    public void Setup()
    {
        _paymentGatewayMock = new Mock<IPaymentGatewayService>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _createKitchenOrderUseCase = new Mock<ICreateKitchenOrderUseCase>();
        
        _checkoutOrderUseCase = new CheckoutOrderUseCase(
            _orderRepositoryMock.Object,
            _paymentGatewayMock.Object,
            _createKitchenOrderUseCase.Object
        );
    }
    
    [Test]
    public async Task FailWhenOrderIsNotFound()
    {
        _orderRepositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Domain.Entities.Order)null!);
        var request = new CheckoutOrderRequest { OrderId = Guid.NewGuid() };
        
        var response = await _checkoutOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeFalse();
        response
            .Notifications
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(new
            {
                Key = nameof(request.OrderId),
                Message = "Order not found"
            });
    }
    
    [Test]
    public async Task FailWhenOrderIsNotConfirmed()
    {
        var order = new Mock<Domain.Entities.Order>();
        order.SetupProperty(o => o.Status, OrderStatus.Accepted);
        _orderRepositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order.Object);
        var request = new CheckoutOrderRequest { OrderId = Guid.NewGuid() };
        
        var response = await _checkoutOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeFalse();
        response
            .Notifications
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(new
            {
                Key = nameof(request.OrderId),
                Message = "Only confirmed orders can be checked out"
            });
    }
    
    [Test]
    public async Task SucceedWhenRequirementsAreMet()
    {
        var orderId = Guid.NewGuid();
        var order = OrderFactory.Create(Guid.NewGuid(), [
            OrderItemFactory.Create(Guid.NewGuid(), "item", 10, 1),
            OrderItemFactory.Create(Guid.NewGuid(), "item2", 5, 3),
        ]);
        order.Confirm();
        _orderRepositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order);
        var captures = new List<PaymentRequest>();
        _paymentGatewayMock
            .Setup(gateway => gateway.Process(Capture.In(captures)))
            .ReturnsAsync(new PaymentResponse("some-transaction-id", PaymentStatus.Success));

        _createKitchenOrderUseCase
            .Setup(createKitchenOrderUseCase => createKitchenOrderUseCase.Execute(It.IsAny<CreateKitchenOrderRequest>()))
            .ReturnsAsync(new CreateKitchenOrderResponse());
        
        var request = new CheckoutOrderRequest { OrderId = orderId  };
        
        var response = await _checkoutOrderUseCase.Execute(request);

        response
            .Should()
            .BeEquivalentTo(new
            {
                IsValid = true,
                TransactionId = "some-transaction-id",
                PaymentStatus = "Success",
                ProcessedAt = order.UpdatedAt
            });
        _orderRepositoryMock
            .Verify(repository => repository.GetByIdAsync(orderId), Times.Once);
        captures
            .Single()
            .Should()
            .BeEquivalentTo(new
            {
                Amount = 25,
                Metadata = new { OrderId = orderId }
            });
    }   
}