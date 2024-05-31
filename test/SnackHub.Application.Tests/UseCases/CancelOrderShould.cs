using FluentAssertions;
using Moq;
using SnackHub.Application.Order.Models;
using SnackHub.Application.Order.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

using OrderFactory = SnackHub.Domain.Entities.Order.Factory;

namespace SnackHub.Application.Tests.UseCases;

public class CancelOrderShould
{
    private Mock<IOrderRepository> _orderRepositoryMock;
    private CancelOrderUseCase _cancelOrderUseCase;
    
    [SetUp]
    public void Setup()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        
        _cancelOrderUseCase = new CancelOrderUseCase(
            _orderRepositoryMock.Object
        );
    }
    
    [Test]
    public async Task FailWhenOrderIsNotFound()
    {
        var request = new CancelOrderRequest
        {
            OrderId = Guid.NewGuid()
        };
        _orderRepositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Domain.Entities.Order)null!);
        
        var response = await _cancelOrderUseCase.Execute(request);
        
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
    public async Task FailWhenOrderIsAlreadyAccepted()
    {
        var order = new Mock<Domain.Entities.Order>();
        order.SetupProperty(o => o.Status, OrderStatus.Accepted);
        var request = new CancelOrderRequest
        {
            OrderId = Guid.NewGuid()
        };
        _orderRepositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order.Object);
        
        var response = await _cancelOrderUseCase.Execute(request);
        
        response
            .Notifications
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(new
            {
                Key = nameof(request.OrderId),
                Message = "Order is already accepted and cannot be cancelled at this time"
            });
    }
    
    [Test]
    public async Task SucceedWhenRequirementsAreMet()
    {
        var order = OrderFactory.Create(Guid.NewGuid(), []);
        var request = new CancelOrderRequest
        {
            OrderId = order.Id
        };
        var captures = new List<Domain.Entities.Order>();
        _orderRepositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(order);
        _orderRepositoryMock
            .Setup(repository => repository.EditAsync(Capture.In(captures)));
        
        var response = await _cancelOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeTrue();
        _orderRepositoryMock
            .Verify(repository => repository.GetByIdAsync(order.Id), Times.Once);
        _orderRepositoryMock
            .Verify(repository => repository.EditAsync(It.IsAny<Domain.Entities.Order>()), Times.Once);
        captures
            .Single()
            .Should()
            .BeEquivalentTo(new
            {
                Id = order.Id,
                Status = OrderStatus.Cancelled
            });
    }
}