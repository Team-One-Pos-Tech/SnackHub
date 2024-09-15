using FluentAssertions;
using Moq;
using SnackHub.Application.Authentication.Models;
using SnackHub.Application.Authentication.UseCases;
using SnackHub.Application.Client.Contracts;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var mockRegisterClientUseCase = new Mock<IRegisterClientUseCase>();

            var signInUseCase = new SignInUseCase(mockSignInFunctionGateway.Object, mockRegisterClientUseCase.Object);

            var request = new SignInRequest("Ednaldo Pereira", "72860763023");

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
    }
}
