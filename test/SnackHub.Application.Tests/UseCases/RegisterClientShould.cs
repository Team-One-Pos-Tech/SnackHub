using Moq;
using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Application.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
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
            var validator = new RegisterClientValidator();
            mockClientRepository = new Mock<IClientRepository>();

            registerClientCase = new RegisterClientUseCase(mockClientRepository.Object, validator);
        }

        [Test]
        public void Register_With_Name()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

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

            // Act
            var response = registerClientCase.Execute(registerClientRequest);

            // Assert
            Assert.That(response.Id, Is.Not.Null);

        }
    }
}