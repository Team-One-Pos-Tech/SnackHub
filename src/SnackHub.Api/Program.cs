using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SnackHub.Application.Payment.Models;
using SnackHub.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = "https://keycloak.forinnovation.com.br/auth/realms/master";
        options.Audience = "snakhubapi";
        options.RequireHttpsMetadata = false; // Defina como true em produção
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Xl38f3J22o6ikk4LnoW8Wyykn1QShZYJ")),
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin() // Permite qualquer origem
                .AllowAnyMethod() // Permite qualquer método HTTP (GET, POST, etc.)
                .AllowAnyHeader(); // Permite qualquer cabeçalho
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SnackHub API", Version = "v1" });

        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://keycloak.forinnovation.com.br/realms/snackhub/protocol/openid-connect/auth"),
                    TokenUrl = new Uri("https://keycloak.forinnovation.com.br/realms/snackhub/protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenID Connect" },
                        { "profile", "User profile" },
                        { "email", "User email" }
                    }
                }
            }
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    }
                },
                new[] { "openid", "profile", "email" }
            }
        });
    });

builder
    .Services
    .AddMongoDb(builder.Configuration)
    .AddRepositories()
    .AddServices()
    .AddUseCases()
    .AddValidators();

var app = builder.Build();

 if (app.Environment.IsDevelopment())
 {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snakhub API v1");

        c.OAuthClientId("snakhubapi");
        c.OAuthClientSecret("Xl38f3J22o6ikk4LnoW8Wyykn1QShZYJ");
        c.OAuthUsePkce();
        c.OAuthAppName("Snackhub - Swagger");
    });
}

if (bool.TryParse(builder.Configuration.GetSection("https").Value, out var result) && result)
    app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMongoDbConventions();
app.MapControllers();
app.Run();