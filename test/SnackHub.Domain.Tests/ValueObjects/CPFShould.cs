using FluentAssertions;
using NUnit.Framework;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Tests.ValueObjects;

public class CPFShould
{
    [Test]
    public void TryParseAsExpected()
    {
        const string cpf = "123.456.789-09";
        
        var result = CPF.TryParse(cpf, out var parsedCpf);
        
        result.Should().BeTrue();
        parsedCpf.Value.Should().Be("12345678909");
    }
}