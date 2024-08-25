using System.Text;
using System.Text.Json;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models;

namespace SnackHub.Infra.Gateways;

public class SignUpFunctionGateway(HttpClient httpClient) : ISignUpFunctionGateway
{
    public async Task Execute(SignUpRequest request)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(request),
            Encoding.Default,
            "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync(
            "http://127.0.0.1:3000/signup",
            jsonContent);
    }
}