using Moq;
using SnackHub.Application.Models;
using SnackHub.Application.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
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
        public void Register_With_Name()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

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
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            registerClientCase.Execute(registerClientRequest);

            // Assert
            var expectedCpf = new CPF(registerClientRequest.CPF);

            mockClientRepository
                .Verify(repository => repository
                    .Add(It.Is<Client>(client => client.CPF.Equals(expectedCpf))),
                        Times.Once);

        }

        [Test]
        public void Validate_Invalid_CPF()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "000.555.414-44");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            var response = registerClientCase.Execute(registerClientRequest);

            // Assert
            response.IsValid.Equals(false);

            mockClientRepository
                .Verify(repository => repository
                    .Add(It.IsAny<Client>()), Times.Never);

        }

        [Test]
        public void Response_With_Id_When_Register_Client()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientUseCase(mockClientRepository.Object);

            // Act
            var response = registerClientCase.Execute(registerClientRequest);

            // Assert
            Assert.That(response.Id, Is.Not.Null);

        }
    }
}