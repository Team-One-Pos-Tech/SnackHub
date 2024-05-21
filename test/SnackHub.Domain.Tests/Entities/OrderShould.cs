using FluentAssertions;
using Moq;
using NUnit.Framework;
using SnackHub.Domain.Base;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

using OrderFactory = SnackHub.Domain.Entities.Order.Factory;
using OrderItemFactory = SnackHub.Domain.ValueObjects.OrderItem.Factory;

namespace SnackHub.Domain.Tests.Entities;

public class OrderShould
{
    [Test]
    public void BeCreatedSuccessfullyWhenRequirementsAreMet()
    {
        var clientId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        
        var order = OrderFactory.Create(clientId, [
            OrderItemFactory.Create(productId, "X-Bacon", 10.0m, 3)
        ]);
        
        order
            .Should()
            .BeEquivalentTo(new
            {
                ClientId = clientId,
                Status = OrderStatus.Pending,
                Items = new []
                {
                    new
                    {
                        ProductId = productId,
                        ProductName = "X-Bacon",
                        UnitPrice = 10.0m,
                        Quantity = 3
                    }
                }
            });
    }
    
    [Test]
    public void ShouldFailOnInvalidClientId()
    {
        var act = () => OrderFactory.Create(Guid.Empty, [
            OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, 3)
        ]);
        
        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Test]
    public void ShouldFailOnInvalidItems()
    {
        var act = () => OrderFactory.Create(Guid.NewGuid(), null!);
        
        act.Should().ThrowExactly<ArgumentNullException>();
    }
    
    [Test]
    public void ShouldBeConfirmedWhenRequirementsAreMet()
    {
        var order = OrderFactory.Create(Guid.NewGuid(), [
            OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, 3)
        ]);
        
        order.Confirm();
        
        order.Status.Should().Be(OrderStatus.Confirmed);
    }
    
    [Test]
    public void ShouldFailOnConfirmWhenStatusIsNotPending()
    {
        var order = new Mock<Domain.Entities.Order>();
        order.SetupGet(o => o.Status).Returns(OrderStatus.Processing);
       
        var act = () => order.Object.Confirm();
        
        act.Should()
            .ThrowExactly<DomainException>()
            .WithMessage("Order cannot be confirmed. Current status: Processing");
    }
    
    [Test]
    public void ShouldFailOnConfirmWhenNoItemsAreSelected()
    {
        var order = OrderFactory.Create(Guid.NewGuid(), []);
        
        var act = () => order.Confirm();
        
        act.Should()
            .ThrowExactly<DomainException>()
            .WithMessage("Order must have at least one item to be confirmed");
    }
    
    [Test]
    public void ShouldBeCancelledWhenRequirementsAreMet(
        [Values(OrderStatus.Pending, OrderStatus.Confirmed, OrderStatus.Cancelled) ] OrderStatus status)
    {
        var order = OrderFactory.Create(Guid.NewGuid(), [
            OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, 3)
        ]);
        
        order.Cancel();
        
        order.Status.Should().Be(OrderStatus.Cancelled);
    }
    
    [Test]
    public void ShouldFailOnCancelWhenStatusIsNotCancellable(
        [Values(OrderStatus.Accepted, OrderStatus.Cancelled, OrderStatus.Declined)] OrderStatus status)
    {
        var order = new Mock<Order>();
        order.SetupGet(o => o.Status).Returns(status);
       
        var act = () => order.Object.Cancel();
        
        act.Should()
            .ThrowExactly<DomainException>()
            .Which
            .Message
            .Should()
            .EndWith("cannot be cancelled");
    }
    
    [Test]
    public void ShouldCalculateTotal()
    {
        var order = OrderFactory.Create(Guid.NewGuid(), [
            OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, 3),
            OrderItemFactory.Create(Guid.NewGuid(), "X-Egg", 5.0m, 2)
        ]);
        
        order.Total.Should().Be(40.0m);
    }
}