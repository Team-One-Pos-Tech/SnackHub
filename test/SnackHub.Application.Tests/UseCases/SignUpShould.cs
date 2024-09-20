using FluentAssertions;
using Moq;
using SnackHub.Application.Authentication.Models;
using SnackHub.Application.Authentication.UseCases;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Application.Tests.UseCases
{
    internal class SignUpShould
    {
        private SignUpUseCase signInUseCase;
        private Mock<ISignUpFunctionGateway> mockSignUpFunctionGateway;
        private Mock<IRegisterClientUseCase> mockRegisterClientUseCase;

        [SetUp]
        public void Setup()
        {
            mockSignUpFunctionGateway = new Mock<ISignUpFunctionGateway>();

            mockSignUpFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignUpRequest>()));

            mockRegisterClientUseCase = new Mock<IRegisterClientUseCase>();

            mockRegisterClientUseCase
                .Setup(useCase => useCase.Execute(It.IsAny<RegisterClientRequest>()))
                .ReturnsAsync(new RegisterClientResponse());

            signInUseCase = new SignUpUseCase(mockSignUpFunctionGateway.Object, mockRegisterClientUseCase.Object);
        }

        [Test]
        public async Task RegisterClient()
        {
            // Arrange

            var request = new SignUpRequest("Ednaldo Pereira", "12345678911", "Default", "email@email.com");

            // Act

            await signInUseCase.Execute(request);

            // Assert

            mockRegisterClientUseCase.Verify(
                gateway => gateway.Execute(It.Is<RegisterClientRequest>(req => req.CPF == request.Cpf)),
                Times.Once
            );

        }

        [Test]
        public async Task ValidateRegisterClient()
        {
            // Arrange

            var response = new RegisterClientResponse();

            response.AddNotification("CPF", "CPF is invalid.");

            mockRegisterClientUseCase
                .Setup(useCase => useCase.Execute(It.IsAny<RegisterClientRequest>()))
                .ReturnsAsync(response);

            var request = new SignUpRequest("Ednaldo Pereira", "12345678911", "Default", "email@email.com");

            // Act

            await signInUseCase.Execute(request);

            // Assert

            mockRegisterClientUseCase.Verify(
                gateway => gateway.Execute(It.IsAny<RegisterClientRequest>()),
                Times.Never
            );

            mockSignUpFunctionGateway.Verify(
                gateway => gateway.Execute(It.IsAny<SignUpRequest>()),
                Times.Never
            );

        }

        [Test]
        public async Task RegisterUserOnAuthenticationProvider()
        {
            // Arrange

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
