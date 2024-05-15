using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Models
{
    public record GetClientResponse(string Name, CPF CPF);
}