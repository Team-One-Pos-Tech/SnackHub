using System.Text;
using System.Text.Json;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models;

namespace SnackHub.Infra.Gateways;

public class SignUpFunctionGateway(HttpClient httpClient) : ISignUpFunctionGateway
{
    public async Task Execute(SignUpRequest request)
    {
        var signUpUrl = Environment.GetEnvironmentVariable("SIGN_UP_FUNCTION_URL");
        
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(request),
            Encoding.Default,
            "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync(
            signUpUrl,
            jsonContent);

        var responseData = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseData);
        }
    }
}