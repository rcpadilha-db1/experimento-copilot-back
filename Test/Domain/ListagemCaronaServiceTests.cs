using Domain.Caronas;
using Domain.Caronas.interfaces;
using Domain.Caronas.Services;
using Domain.Usuarios;
using Domain.Veiculos;
using FluentAssertions;
using Moq;
using Xunit;

namespace Test.Domain;

public class ListagemCaronaServiceTests
{
    private readonly Mock<ICaronaRepository> _caronaRepositoryMock;
    private readonly ListagemCaronaService _service;

    public ListagemCaronaServiceTests()
    {
        _caronaRepositoryMock = new Mock<ICaronaRepository>();
        _service = new ListagemCaronaService(_caronaRepositoryMock.Object);
    }

    [Fact]
    public async Task ListarCaronasAsync_QuandoNaoEncontra_DeveRetornarListaVazia()
    {
        //arrange
        _caronaRepositoryMock
            .Setup(repo => repo.ListarCaronasPorUsuarioAsync("u001"))
            .ReturnsAsync([]);

        //act
        var result = await _service.ListarCaronasAsync("u001");

        //assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ListarCaronasAsync_QuandoHaItens_DeveRetornarLista()
    {
        //arrange
        var dataExperada = DateTime.Now;
        var caronas = new List<Carona>
        {
            new Carona
            {
                Veiculo = new Veiculo { Placa = "ABC1234", Usuario = new Usuario { Nome = "John Doe" } },
                Data = dataExperada
            }
        };
        _caronaRepositoryMock
            .Setup(repo => repo.ListarCaronasPorUsuarioAsync("u001"))
            .ReturnsAsync(caronas);

        //act
        var result = await _service.ListarCaronasAsync("u001");

        //assert
        result.Should().HaveCount(1);
        result[0].Placa.Should().Be("ABC1234");
        result[0].Nome.Should().Be("John Doe");
        result[0].Data.Should().Be(dataExperada);
    }
}