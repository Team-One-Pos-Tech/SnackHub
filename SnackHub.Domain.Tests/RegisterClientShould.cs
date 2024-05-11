using Moq;
using SnackHub.Application.Tests;

namespace SnackHub.Domain.Tests
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
            var registerClientRequest = new RegisterClientRequest(Name: "John Doe");

            var mockClientRepository = new Mock<IClientRepository>();

            var registerClientCase = new RegisterClientCase(mockClientRepository.Object);

            // Act
            registerClientCase.Execute(registerClientRequest);

            // Assert
            mockClientRepository
                .Verify(repository => repository
                    .Add(It.Is<Client>(client => client.Name == registerClientRequest.Name)), 
                        Times.Once);

        }
    }
}