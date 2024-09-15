using System.Text;
using System.Text.Json;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Infra.Gateways;

public class SignUpFunctionGateway(HttpClient httpClient, string signUpFunctionUrl) : ISignUpFunctionGateway
{
    public async Task Execute(SignUpRequest request)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(request),
            Encoding.Default,
            "application/json");

        using HttpResponseMessage response = await httpClient.PostAsync(
            signUpFunctionUrl,
            jsonContent);

        var responseData = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseData);
        }
    }
}