using FluentAssertions;
using Moq;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Application.Client.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases
{
    public class RegisterClientShould
    {
        IRegisterClientUseCase registerClientCase;
        Mock<IClientRepository> mockClientRepository;

        [SetUp]
        public void Setup()
        {
            mockClientRepository = new Mock<IClientRepository>();
            var validator = new RegisterClientValidator(mockClientRepository.Object);

            registerClientCase = new RegisterClientUseCase(mockClientRepository.Object, validator);
        }

        [Test]
        public async Task Register_With_Name()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            registerClientRequest.Email = "email";

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
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            registerClientRequest.Email = "email";

            // Act
            await registerClientCase.Execute(registerClientRequest);

            // Assert
            var expectedCpf = new CPF("72860763023");

            mockClientRepository
                .Verify(repository => repository
                    .AddAsync(It.Is<Domain.Entities.Client>(client => client.CPF.Equals(expectedCpf))),
                        Times.Once);

        }

        [Test]
        public async Task Validate_Invalid_CPF()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "000.555.414-44");

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
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

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