using SnackHub.Application.Contracts;
using SnackHub.Application.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: change to AddScoped when using mongo db
builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();
builder.Services.AddScoped<IGetClientUseCase, GetClientUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();