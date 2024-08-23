namespace SnackHub.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Net.Http.Headers;
    using SnackHub.Configuration;
    using System.IdentityModel.Tokens.Jwt;

    public static class AuthenticationExtension
    {
        public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var auth = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var configKeycloak = configuration.GetSection("Keycloak").Get<KeycloakSetting>();

            foreach (var keyCloack in configKeycloak.Realms)
            {
                auth.AddJwtBearer(keyCloack.Name, cfg =>
                {
                    cfg.RequireHttpsMetadata = true;

                    cfg.Authority = keyCloack.Authority;

                    cfg.IncludeErrorDetails = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = keyCloack.Issuer,
                        ValidateLifetime = true
                    };
                });
            }

            auth.AddPolicyScheme(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var realms = configKeycloak.Realms.Select(x => x.Name).ToList();

                    string scheme = "snackhub";

                    string authorization = context.Request.Headers[HeaderNames.Authorization];

                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                    {
                        var token = authorization.Substring("Bearer ".Length).Trim();
                        var jwtHandler = new JwtSecurityTokenHandler();

                        if (jwtHandler.CanReadToken(token))
                        {
                            var issue = jwtHandler.ReadJwtToken(token).Issuer;
                        }
                    }

                    return scheme;
                };
            });
        }
    }
}
