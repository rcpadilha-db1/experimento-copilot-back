using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Caronas;
using Domain.Caronas.interfaces;
using Domain.Caronas.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Test.Domain;

public class RemocaoCaronaServiceTests
{
    private readonly Mock<ICaronaRepository> _caronaRepositoryMock;
    private readonly RemocaoCaronaService _service;

    public RemocaoCaronaServiceTests()
    {
        _caronaRepositoryMock = new Mock<ICaronaRepository>();
        _service = new RemocaoCaronaService(_caronaRepositoryMock.Object);
    }

    [Fact]
    public async Task RemoverAsync_QuandoNaoEncontraCarona_DeveRetornarErro()
    {
        //arrange
        _caronaRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(It.IsAny<string>()))
            .ReturnsAsync((Carona) null);

        //act
        Func<Task> act = async () => await _service.RemoverAsync("u001");

        //assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage(string.Format(MensagensErro.CaronaNaoEncontrada, "u001"));
    }

    [Fact]
    public async Task RemoverAsync_ShouldCallRemoverAsync_WhenCaronaExists()
    {
        //arrange
        var carona = new Carona { Id = "u001" };
        _caronaRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync("u001"))
            .ReturnsAsync(carona);

        //act
        await _service.RemoverAsync("u001");

        //assert
        _caronaRepositoryMock.Verify(repo => repo.RemoverAsync(carona), Times.Once);
    }
}