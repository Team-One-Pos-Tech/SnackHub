using FluentAssertions;
using Moq;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;
using System.Xml.Linq;

namespace SnackHub.Application.Tests.UseCases
{
    public class GetClientShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get()
        {
            // Arrange
            var mockClientRepository = new Mock<IClientRepository>();

            var getClientUseCase = new GetClientUseCase(mockClientRepository.Object);

            var id = Guid.NewGuid();

            var clientMock = new Client("Ednaldo Pereira", new CPF("728.607.630-23"));

            mockClientRepository.Setup(repository => repository.Get(It.IsAny<Guid>()))
                .Returns(clientMock);

            // Act
            var response = getClientUseCase.Execute(id);

            // Assert
            response.Should().NotBeNull(); 

            response.Name.Should().Be(clientMock.Name);
            response.CPF.Should().Be(clientMock.CPF);

        }
    }
}