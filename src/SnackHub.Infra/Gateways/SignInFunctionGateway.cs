using System.Text;
using System.Text.Json;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Infra.Gateways;

public class SignInFunctionGateway(HttpClient httpClient) : ISignInFunctionGateway
{
    public async Task<AuthResponseType> Execute(SignInRequest request)
    {
        var signInUrl = Environment.GetEnvironmentVariable("SIGN_IN_FUNCTION_URL");
        
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(request),
            Encoding.Default,
            "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync(
            signInUrl,
            jsonContent);

        var responseHttp = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseHttp);
        }
        
        var authResponse = JsonSerializer.Deserialize<AuthResponseType>(responseHttp);
        
        return authResponse!;
    }
}