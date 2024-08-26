using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Crosscutting.Requests;
using Domain.Caronas;
using Domain.Caronas.interfaces;
using Domain.Caronas.Services;
using Domain.Usuarios.Interfaces;
using Domain.Veiculos;
using Domain.Veiculos.Interfaces;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Test.Domain;

public class CadastroCaronaServiceTests
{
    private readonly Mock<ICadastroCaronaValidator> _validatorMock = new ();
    private readonly Mock<ICaronaRepository> _repositoryMock = new ();
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock = new ();
    private readonly Mock<IVeiculoRepository> _veiculoRepositoryMock = new ();
    private readonly CadastroCaronaService _service;

    public CadastroCaronaServiceTests()
    {
        _service = new CadastroCaronaService(
            _validatorMock.Object,
            _repositoryMock.Object,
            _usuarioRepositoryMock.Object,
            _veiculoRepositoryMock.Object);
    }

    [Fact]
    public async Task CadastrarAsync_ComRequestValido_DeveCadastrar()
    {
        // Arrange
        var request = new CaronaRequest
        {
            VeiculoId = "veiculoId",
            UsuarioId = "usuarioId",
            Data = DateTime.Now
        };

        var veiculo = new Veiculo()
        {
            Id = request.VeiculoId,
            Capacidade = 4,
            Placa = "ABC1234",
            Caronas = new List<Carona>()
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());
        _veiculoRepositoryMock
            .Setup(v => v.VeiculoExisteAsync(request.VeiculoId))
            .ReturnsAsync(true);
        _usuarioRepositoryMock
            .Setup(r => r.UsuarioExisteAsync(request.UsuarioId))
            .ReturnsAsync(true);
        _repositoryMock
            .Setup(r => r.ExisteCaronaPorDiaEUsuarioAsync(request.Data, request.UsuarioId))
            .ReturnsAsync(false);
        _veiculoRepositoryMock
            .Setup(v => v.ObterPorIdComCaronasAsync(request.VeiculoId))
            .ReturnsAsync(veiculo);

        // Act
        var result = await _service.CadastrarAsync(request);

        // Assert
        result.Should().NotBeEmpty();
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Carona>()), Times.Once);
    }

    [Fact]
    public async Task CadastrarAsync_ComRequestInvalido_DeveRetornarExcecao()
    {
        // Arrange
        var request = new CaronaRequest();
        
        var validationResult = new ValidationResult();
        
        validationResult.Errors.Add(new ValidationFailure("VeiculoId", "VeiculoId é obrigatório"));

        _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

        // Act
        var exception = await Assert.ThrowsAsync<ValidateException>(() => _service.CadastrarAsync(request));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("VeiculoId é obrigatório", exception.Message);
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Carona>()), Times.Never);
    }
    
    [Fact]
    public async Task CadastrarAsync_QuandoNaoEncontrarUsuario_DeveRetornarExcecao()
    {
        // Arrange
        var request = new CaronaRequest
        {
            VeiculoId = "veiculoId",
            UsuarioId = "usuarioId",
            Data = DateTime.Now
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());
        _usuarioRepositoryMock
            .Setup(r => r.UsuarioExisteAsync(request.UsuarioId))
            .ReturnsAsync(false);
        
        // Act
        Func<Task> action = async () => await _service.CadastrarAsync(request);

        // Assert
        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage(string.Format(MensagensErro.UsuarioNaoEncontrado, request.UsuarioId));
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Carona>()), Times.Never);
    }
    
    [Fact]
    public async Task CadastrarAsync_QuandoNaoEncontrarVeiculo_DeveRetornarExcecao()
    {
        // Arrange
        var request = new CaronaRequest
        {
            VeiculoId = "veiculoId",
            UsuarioId = "usuarioId",
            Data = DateTime.Now
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());
        _usuarioRepositoryMock
            .Setup(r => r.UsuarioExisteAsync(request.UsuarioId))
            .ReturnsAsync(true);
        _veiculoRepositoryMock
            .Setup(v => v.VeiculoExisteAsync(request.VeiculoId))
            .ReturnsAsync(false);
        
        // Act
        Func<Task> action = async () => await _service.CadastrarAsync(request);

        // Assert
        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage(string.Format(MensagensErro.VeiculoNaoEncontrado, request.VeiculoId));
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Carona>()), Times.Never);
    }
    
    [Fact]
    public async Task CadastrarAsync_QuandoVeiculoLotado_DeveRetornarExcecao()
    {
        // Arrange
        var request = new CaronaRequest
        {
            VeiculoId = "veiculoId",
            UsuarioId = "usuarioId",
            Data = DateTime.Now
        };
        
        var veiculo = new Veiculo()
        {
            Id = request.VeiculoId,
            Capacidade = 2,
            Placa = "ABC1234",
            Caronas = new List<Carona>()
            {
                new Carona() { Data = DateTime.Now},
                new Carona() { Data = DateTime.Now}
            }
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());
        _usuarioRepositoryMock
            .Setup(r => r.UsuarioExisteAsync(request.UsuarioId))
            .ReturnsAsync(true);
        _veiculoRepositoryMock
            .Setup(v => v.VeiculoExisteAsync(request.VeiculoId))
            .ReturnsAsync(true);
        _veiculoRepositoryMock
            .Setup(v => v.ObterPorIdComCaronasAsync(request.VeiculoId))
            .ReturnsAsync(veiculo);
        
        // Act
        Func<Task> action = async () => await _service.CadastrarAsync(request);

        // Assert
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage(string.Format(MensagensErro.VeiculoLotado, veiculo.Placa));
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Carona>()), Times.Never);
    }
    
    [Fact]
    public async Task CadastrarAsync_QuandoUsuarioJaPossuiCarona_DeveRetornarExcecao()
    {
        // Arrange
        var request = new CaronaRequest
        {
            VeiculoId = "veiculoId",
            UsuarioId = "usuarioId",
            Data = DateTime.Now
        };
        
        var veiculo = new Veiculo()
        {
            Id = request.VeiculoId,
            Capacidade = 2,
            Placa = "ABC1234",
            Caronas = new List<Carona>()
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());
        _usuarioRepositoryMock
            .Setup(r => r.UsuarioExisteAsync(request.UsuarioId))
            .ReturnsAsync(true);
        _veiculoRepositoryMock
            .Setup(v => v.VeiculoExisteAsync(request.VeiculoId))
            .ReturnsAsync(true);
        _veiculoRepositoryMock
            .Setup(v => v.ObterPorIdComCaronasAsync(request.VeiculoId))
            .ReturnsAsync(veiculo);
        _repositoryMock
            .Setup(r => r.ExisteCaronaPorDiaEUsuarioAsync(request.Data, request.UsuarioId))
            .ReturnsAsync(true);
        
        // Act
        Func<Task> action = async () => await _service.CadastrarAsync(request);

        // Assert
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage(string.Format(MensagensErro.UsuarioJaPossuiCaronaMarcada, request.UsuarioId));
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Carona>()), Times.Never);
    }
}