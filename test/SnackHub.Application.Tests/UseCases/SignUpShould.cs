using FluentAssertions;
using Moq;
using SnackHub.Application.Authentication.Models;
using SnackHub.Application.Authentication.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Application.Tests.UseCases
{
    internal class SignUpShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task RegisterUserOnAuthenticationProvider()
        {
            // Arrange

            var mockSignUpFunctionGateway = new Mock<ISignUpFunctionGateway>();

            mockSignUpFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignUpRequest>()));

            var signInUseCase = new SignUpUseCase(mockSignUpFunctionGateway.Object);

            var request = new SignUpRequest("Ednaldo Pereira", "12345678911", "Default", "email@email.com");

            // Act

            await signInUseCase.Execute(request);

            // Assert

            mockSignUpFunctionGateway.Verify(
                gateway => gateway.Execute(It.Is<SignUpRequest>(req => req.Cpf == request.Cpf)),
                Times.Once
            );

        }
    }
}
