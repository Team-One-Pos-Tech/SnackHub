using System.Reflection;
using Microsoft.OpenApi.Models;
using SnackHub.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddAuthenticationExtension(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Snack Hub API", Version = "v1" });
        options.AddAuthorizationOptions();
        
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        
    })
    .AddHttpClient();


builder
    .Services
    .AddMongoDb(builder.Configuration)
    .AddRepositories()
    .AddServices()
    .AddUseCases()
    .AddValidators()
    .AddGateways(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMongoDbConventions();
app.MapControllers();
app.Run();