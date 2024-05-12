using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Tests.UseCases
{
    public record GetClientResponse(string Name, CPF CPF);
}