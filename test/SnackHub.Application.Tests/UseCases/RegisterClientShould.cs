using FluentAssertions;
using Moq;
using SnackHub.Application.Client.Models;
using SnackHub.Application.Client.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases
{
    public class RegisterClientShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Register_With_Name()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(Name: "John Doe", CPF: "728.607.630-23");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            await registerClientCase.Execute(registerClientRequest);

            // Assert
            mockClientRepository
                .Verify(repository => repository
                    .AddAsync(It.Is<Domain.Entities.Client>(client => client.Name == registerClientRequest.Name)),
                        Times.Once);

        }

        [Test]
        public async Task Register_With_CPF()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(Name: "John Doe", CPF: "728.607.630-23");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            await registerClientCase.Execute(registerClientRequest);

            // Assert
            var expectedCpf = new CPF(registerClientRequest.CPF);

            mockClientRepository
                .Verify(repository => repository
                    .AddAsync(It.Is<Domain.Entities.Client>(client => client.CPF.Equals(expectedCpf))),
                        Times.Once);

        }

        [Test]
        public async Task Validate_Invalid_CPF()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(Name: "John Doe", CPF: "000.555.414-44");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            var response = await registerClientCase.Execute(registerClientRequest);

            // Assert
            response
                .IsValid
                .Should()
                .Be(false);

            mockClientRepository
                .Verify(repository => repository
                    .AddAsync(It.IsAny<Domain.Entities.Client>()), Times.Never);

        }

        [Test]
        public void Response_With_Id_When_Register_Client()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(Name: "John Doe", CPF: "728.607.630-23");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            var response = registerClientCase.Execute(registerClientRequest);

            // Assert
            response
                .Should()
                .NotBeNull();
            
            response
                .Id
                .Should()
                .NotBe(null);
        }
    }
}