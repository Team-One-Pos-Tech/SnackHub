using Moq;
using SnackHub.Application.Client.Models;
using SnackHub.Application.Client.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases
{
    public class RegisterClientValidatorShould
    {
        RegisterClientValidator _registerClientValidator;
        
        [SetUp]
        public void Setup()
        {
            var clientReposioryMock = new Mock<IClientRepository>();
            _registerClientValidator = new RegisterClientValidator(clientReposioryMock.Object);
        }

        [Test]
        public void Validate_With_Valid_CPF()
        {
            // Arrange

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            var response = new RegisterClientResponse();

            // Act
            var isValid = _registerClientValidator.IsValid(registerClientRequest, out response);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Validate_Invalid_CPF()
        {
            // Arrange

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "111.111.111-23");

            var response = new RegisterClientResponse();

            // Act
            var isValid = _registerClientValidator.IsValid(registerClientRequest, out response);

            // Assert
            Assert.That(isValid, Is.False);

            Assert.Multiple(() =>
            {
                Assert.That(response.Notifications.First().Key, Is.EqualTo("CPF"));
                Assert.That(response.Notifications.First().Message, Is.EqualTo("CPF is invalid."));
            });
        }

        [Test]
        public async Task Validate_If_CPF_Is_Already_Registred()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");
            var response = new RegisterClientResponse();

            // Act
            var isValid = await _registerClientValidator.IsValid(registerClientRequest, out response);

            // Assert
            Assert.IsFalse(isValid);

            Assert.Multiple(() =>
            {
                Assert.That(response.Notifications.First().Key, Is.EqualTo("CPF"));
                Assert.That(response.Notifications.First().Message, Is.EqualTo("CPF is already registered."));
            });

        }
    }
}
