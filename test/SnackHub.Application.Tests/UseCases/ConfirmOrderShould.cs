using FluentAssertions;
using Moq;
using SnackHub.Application.Order.Models;
using SnackHub.Application.Order.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.Tests.UseCases;

public class ConfirmOrderShould
{
    private Mock<IClientRepository> _clientRepositoryMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IOrderRepository> _orderRepositoryMock;
    private ConfirmOrderUseCase _confirmOrderUseCase;
    
    [SetUp]
    public void Setup()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        
        _confirmOrderUseCase = new ConfirmOrderUseCase(
            _clientRepositoryMock.Object,
            _productRepositoryMock.Object,
            _orderRepositoryMock.Object
        );
    }
    
    [Test]
    public async Task FailWhenClientIsNotFound()
    {
        var request = new ConfirmOrderRequest
        {
            ClientId = Guid.NewGuid(),
            Items = []
        };
        _clientRepositoryMock
            .Setup(repository => repository.ExistsByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        var response = await _confirmOrderUseCase.Execute(request);
        
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
                Key = nameof(request.ClientId),
                Message = "Client not found"
            });
    }

    [Test]
    public async Task FailWhenSomeProductIsNotFound()
    {
        var request = new ConfirmOrderRequest
        {
            ClientId = Guid.NewGuid(),
            Items = [new ConfirmOrderRequest.Item(Guid.NewGuid(), 1)]
        };
        _clientRepositoryMock
            .Setup(repository => repository.ExistsByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _productRepositoryMock
            .Setup(repository => repository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync([]);
        
        var response = await _confirmOrderUseCase.Execute(request);
        
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
                Key = nameof(request.Items),
                Message = "One or more products could not be found"
            });
    }

    [Test]
    public async Task FailOnInvalidOrder()
    {
        var request = new ConfirmOrderRequest
        {
            ClientId = Guid.NewGuid(),
            Items = []
        };
        _clientRepositoryMock
            .Setup(repository => repository.ExistsByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _productRepositoryMock
            .Setup(repository => repository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync([]);
        
        var response = await _confirmOrderUseCase.Execute(request);
        
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
                Key = "Order",
                Message = "Order must have at least one item to be confirmed"
            });
    }

    [Test]
    public async Task SucceedWhenRequirementsAreMet()
    {
        var clientId = Guid.NewGuid();
        var product = new Product("Product", Category.Lanche, 10m, "Description", []);
        var request = new ConfirmOrderRequest
        {
            ClientId = clientId,
            Items = [new ConfirmOrderRequest.Item(product.Id, 1)]
        };
        _productRepositoryMock
            .Setup(repository => repository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync([product]);
        _clientRepositoryMock
            .Setup(repository => repository.ExistsByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        var captures = new List<Domain.Entities.Order>();
        _orderRepositoryMock.Setup(repository => repository.AddAsync(Capture.In(captures)));
        
        var response = await _confirmOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeTrue();
        _clientRepositoryMock
            .Verify(repository => repository.ExistsByIdAsync(clientId), Times.Once);
        _productRepositoryMock
            .Verify(repository => repository.GetByIdsAsync(new[] { product.Id }), Times.Once);
        _orderRepositoryMock
            .Verify(repository => repository.AddAsync(It.IsAny<Domain.Entities.Order>()), Times.Once);
        captures
            .Single()
            .Should()
            .BeEquivalentTo(new
            {
                response.Id,
                ClientId = clientId,
                Items = new[]
                {
                    new
                    {
                        ProductId = product.Id,
                        ProductName = "Product",
                        UnitPrice = 10m,
                        Quantity = 1
                    }
                }
            });
    }
}