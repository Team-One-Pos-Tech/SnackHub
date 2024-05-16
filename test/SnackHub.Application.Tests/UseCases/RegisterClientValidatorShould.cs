using SnackHub.Application.Client.Models;
using SnackHub.Application.Client.UseCases;

namespace SnackHub.Application.Tests.UseCases
{
    public class RegisterClientValidatorShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validate_With_Valid_CPF()
        {
            // Arrange
            var validator = new RegisterClientValidator();

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23");

            var response = new RegisterClientResponse();

            // Act
            var isValid = validator.IsValid(registerClientRequest, out response);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Validate_Invalid_CPF()
        {
            // Arrange
            var validator = new RegisterClientValidator();

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "111.111.111-23");

            var response = new RegisterClientResponse();

            // Act
            var isValid = validator.IsValid(registerClientRequest, out response);

            // Assert
            Assert.That(isValid, Is.False);

            Assert.Multiple(() =>
            {
                Assert.That(response.Notifications.First().Key, Is.EqualTo("CPF"));
                Assert.That(response.Notifications.First().Message, Is.EqualTo("CPF is invalid."));
            });
        }
    }
}
