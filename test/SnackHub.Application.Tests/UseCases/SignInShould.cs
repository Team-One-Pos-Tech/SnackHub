using FluentAssertions;
using Moq;
using SnackHub.Application.Authentication.Models;
using SnackHub.Application.Authentication.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Application.Tests.UseCases
{
    internal class SignInShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AuthenticateUser()
        {
            // Arrange

            var mockSignInFunctionGateway = new Mock<ISignInFunctionGateway>();

            mockSignInFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignInRequest>()))
                .ReturnsAsync(new AuthResponseType("token", true));

            var signInUseCase = new SignInUseCase(mockSignInFunctionGateway.Object);

            var request = new SignInRequest("72860763023", "123");

            // Act

            SignInResponse response = await signInUseCase.Execute(request);

            // Assert

            response
                .Should()
                .NotBeNull();

            response
                .IdToken
                .Should()
                .NotBeNull();

        }

        [Test]
        public async Task AuthenticateAnonymousUserWhenCpfIsEmpty()
        {
            // Arrange

            var anonymousCpf = "00000000000";

            var mockSignInFunctionGateway = new Mock<ISignInFunctionGateway>();

            mockSignInFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignInRequest>()))
                .ReturnsAsync(new AuthResponseType("token", true));

            var signInUseCase = new SignInUseCase(mockSignInFunctionGateway.Object);

            var request = new SignInRequest("", "DefaultPassword");

            // Act

            SignInResponse response = await signInUseCase.Execute(request);

            // Assert

            mockSignInFunctionGateway.Verify(
                gateway => gateway.Execute(It.Is<SignInRequest>(req => req.Username == anonymousCpf)),
                Times.Once
            );

            response
                .Should()
                .NotBeNull();

            response
                .IdToken
                .Should()
                .NotBeNull();

        }
    }
}
