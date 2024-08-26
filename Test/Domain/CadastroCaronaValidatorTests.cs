using Crosscutting.Requests;
using Domain.Caronas.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Test.Domain;

public class CadastroCaronaValidatorTests
{
    private readonly CadastroCaronaValidator _validator = new CadastroCaronaValidator();

    [Fact]
    public void Validate_QuandoVeiculoNaoInformado_DeveRetornarErro()
    {
        //arrange
        var request = new CaronaRequest
        {
            VeiculoId = string.Empty,
            UsuarioId = "1",
            Data = DateTime.Now
        };
        
        //act
        var result = _validator.TestValidate(request);

        //assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(c => c.VeiculoId);
    }

    [Fact]
    public void Validate_QuandoUsuarioNaoInformado_DeveRetornarErro()
    {
        //arrange
        var request = new CaronaRequest
        {
            VeiculoId = "v01",
            UsuarioId = string.Empty,
            Data = DateTime.Now
        };
        
        //act
        var result = _validator.TestValidate(request);

        //assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(c => c.UsuarioId);
    }

    [Fact]
    public void Validate_QuandoDataNaoInformado_DeveRetornarErro()
    {
        //arrange
        var request = new CaronaRequest
        {
            VeiculoId = "v01",
            UsuarioId = string.Empty
        };
        
        //act
        var result = _validator.TestValidate(request);

        //assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(c => c.Data);
    }

    [Fact]
    public void Validate_QuandoDataPassada_DeveRetornarErro()
    {
        //arrange
        var request = new CaronaRequest
        {
            VeiculoId = "v01",
            UsuarioId = "u01",
            Data = DateTime.Now.AddDays(-1)
        };
        
        //act
        var result = _validator.TestValidate(request);

        //assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(c => c.Data);
    }

    [Fact]
    public void Validate_QuandoRequestValido_DeveRetornarSucesso()
    {
        //arrange
        var request = new CaronaRequest
        {
            VeiculoId = "v01",
            UsuarioId = "u01",
            Data = DateTime.Now
        };
        
        //act
        var result = _validator.TestValidate(request);

        //assert
        result.IsValid.Should().BeTrue();
        result.ShouldNotHaveAnyValidationErrors();
    }
}