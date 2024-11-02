using SnackHub.Configuration;
using SnackHub.Domain.Contracts;
using SnackHub.Infra.Gateways;

namespace SnackHub.Extensions;

public static class GatewaysExtensions
{
    public static IServiceCollection AddGateways(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Auth:Lambda").Get<AuthLambdaSettings>()!;

        var signUpFunctionGateway =
            new SignUpFunctionGateway(new HttpClient(), settings.SignUpUrl);

        var signInFunctionGateway =
            new AuthService(new HttpClient(), settings.SignInUrl);

        serviceCollection.AddSingleton<ISignUpFunctionGateway>(_ => signUpFunctionGateway);
        serviceCollection.AddSingleton<IAuthService>(_ => signInFunctionGateway);

        return serviceCollection;
    }
}