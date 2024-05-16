using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Client.Models
{
    public record GetClientResponse(string Name, CPF CPF);
}