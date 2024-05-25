using FluentAssertions;
using Moq;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.Tests.UseCases;

public class GetByCategoryShould
{
    private GetByCategoryUseCase _getByCategoryUseCase;
    private Mock<IProductRepository> _productRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _getByCategoryUseCase = new GetByCategoryUseCase(_productRepositoryMock.Object);
    }

    [Test]
    public async Task Get()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var category = Category.Lanche;
        
        var product1 = new Product(
            "Product 1", 
            category, 
            10, 
            "Description 1",
            ["image1.jpg"],
            productId
        );
        
        var product2 = new Product(
            "Product 2", 
            category, 
            10, 
            "Description 2",
            ["image2.jpg"],
            productId
        );

        _productRepositoryMock.Setup(repository => 
                repository.GetByCategory(category))
            .ReturnsAsync([product1, product2]);
        
        // Act
        var productsResponse = await _getByCategoryUseCase.Get(category);
        
        // Assert
        productsResponse
            .Count()
            .Should()
            .Be(2);

        productsResponse.First().Id.Should().Be(product1.Id);
        productsResponse.First().Category.Should().Be(product1.Category);
        productsResponse.First().Name.Should().Be(product1.Name);
        
        productsResponse.Last().Id.Should().Be(product2.Id);
        productsResponse.Last().Category.Should().Be(product2.Category);
        productsResponse.Last().Name.Should().Be(product2.Name);
    }
}

public class GetByCategoryUseCase(IProductRepository productRepository)
{
    public Task<IEnumerable<GetProductResponse>> Get(Category category)
    {
        throw new NotImplementedException();
    }
}