using Moq;
using SnackHub.Application.Adapters;
using SnackHub.Application.Models;
using SnackHub.Application.UseCases;
using SnackHub.Domain.Entities;

namespace SnackHub.Application.Tests.UseCases
{
    public class RegisterClientShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Register_With_Name()
        {
            // Arrange
            var registerClientRequest = new RegisterRequest(Name: "John Doe", CPF: "000.555.414-44");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterUseCase(mockClientRepository.Object);

            // Act
            registerClientCase.Execute(registerClientRequest);

            // Assert
            mockClientRepository
                .Verify(repository => repository
                    .Add(It.Is<Client>(client => client.Name == registerClientRequest.Name)),
                        Times.Once);

        }

        [Test]
        public void Register_With_CPF()
        {
            // Arrange
            var registerClientRequest = new RegisterRequest(Name: "John Doe", CPF: "000.555.414-44");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterUseCase(mockClientRepository.Object);

            // Act
            registerClientCase.Execute(registerClientRequest);

            // Assert
            mockClientRepository
                .Verify(repository => repository
                    .Add(It.Is<Client>(client => client.CPF == registerClientRequest.CPF)),
                        Times.Once);

        }
    }
}