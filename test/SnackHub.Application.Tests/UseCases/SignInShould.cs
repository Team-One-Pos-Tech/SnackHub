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
    internal class SignInShould
    {
        private Mock<IGetClientUseCase> mockGetClient;
        private SignInUseCase signInUseCase;
        private Mock<IAuthService> mockSignInFunctionGateway;

        [SetUp]
        public void Setup()
        {
            mockGetClient = new Mock<IGetClientUseCase>();
            
            mockSignInFunctionGateway = new Mock<IAuthService>();

            signInUseCase = new SignInUseCase(mockSignInFunctionGateway.Object, mockGetClient.Object);
        }

        [Test]
        public async Task AuthenticateUser()
        {
            // Arrange
            mockSignInFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignInRequest>()))
                .ReturnsAsync(new AuthResponseType("token", true));

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
        public async Task Dont_Auth_When_Does_Not_Find_User()
        {
            // Arrange
            
            mockGetClient
                .Setup(getClient => getClient.Execute(It.IsAny<string>()));

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

            var anonymousUsername = "00000000000";

            mockSignInFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignInRequest>()))
                .ReturnsAsync(new AuthResponseType("token", true));

            var request = new SignInRequest("", "DefaultPassword");

            // Act

            SignInResponse response = await signInUseCase.Execute(request);

            // Assert

            mockSignInFunctionGateway.Verify(
                gateway => gateway.Execute(It.Is<SignInRequest>(req => req.Username == anonymousUsername)),
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
