using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace SnackHub.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationExtension(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var cognitoIssuer = $"https://cognito-idp.{configuration["AWS:Region"]}.amazonaws.com/{configuration["AWS:UserPoolId"]}";
        var audience = configuration["AWS:ClientId"];
        
        serviceCollection.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = cognitoIssuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                {
                    var json = new WebClient().DownloadString($"{cognitoIssuer}/.well-known/jwks.json");
                    var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                    return keys;
                }
            };
        });

        return serviceCollection;
    }
}