using Moq;
using RotasAereasOtimizadas.Application.Services;
using RotasAereasOtimizadas.Domain.Model;
using RotasAereasOtimizadas.Domain.Repository;
using RotasAereasOtimizadas.Domain.Service;

namespace RotasAereasOtimizadas.Tests
{
    /// <summary>
    /// Teste unitários rotas otimizadas
    /// </summary>
    public class RotaOtimizadaTest
    {
        private readonly Mock<RotaOtimizadaService> _rotaOtimizadaService;
        private readonly Mock<RotasAereasService> _rotaAereaService;
        private readonly Mock<IRotaAereaRepository> _repository;

        public RotaOtimizadaTest()
        {
            _repository = new Mock<IRotaAereaRepository>();
            _rotaOtimizadaService = new Mock<RotaOtimizadaService>(_repository.Object);
            _rotaAereaService = new Mock<RotasAereasService>(_repository.Object, _rotaOtimizadaService.Object);

            var rotas = new List<RotaAerea>()
            {
                new RotaAerea(){Id = 1, IdAeroportoDestino = 2, IdAeroportoOrigem = 1, NomeAeroportoDestino = "nome aeroporto 1", 
                    NomeAeroportoOrigem = "nome aeroporto 1", Valor = 10},

                new RotaAerea(){Id = 2, IdAeroportoDestino = 3, IdAeroportoOrigem = 2, NomeAeroportoDestino = "nome aeroporto 3",
                    NomeAeroportoOrigem = "nome aeroporto 2", Valor = 20},

                new RotaAerea(){Id = 3, IdAeroportoDestino = 1, IdAeroportoOrigem = 3, NomeAeroportoDestino = "nome aeroporto 2", 
                    NomeAeroportoOrigem = "nome aeroporto 1", Valor = 30},

                new RotaAerea(){Id = 4, IdAeroportoDestino = 3, IdAeroportoOrigem = 1, NomeAeroportoDestino = "nome aeroporto 2", 
                    NomeAeroportoOrigem = "nome aeroporto 1", Valor = 60}
            };

            _repository.Setup(s => s.GetRotaAerea(1)).ReturnsAsync(rotas.FindAll(r => r.IdAeroportoOrigem == 1));
            _repository.Setup(s => s.GetRotaAerea(2)).ReturnsAsync(rotas.FindAll(r => r.IdAeroportoOrigem == 2));
            _repository.Setup(s => s.GetRotaAerea(3)).ReturnsAsync(rotas.FindAll(r => r.IdAeroportoOrigem == 3));
        }

        [Fact]
        public async Task FindRotaOtimizada_ShouldReturnRotaOtimizada()
        {

            var rotas = new List<RotaAerea>()
            {
                new RotaAerea(){Id = 1, IdAeroportoDestino = 2, IdAeroportoOrigem = 1, NomeAeroportoDestino = "nome aeroporto 1", NomeAeroportoOrigem = "nome aeroporto 1", Valor = 10},
                new RotaAerea(){Id = 4, IdAeroportoDestino = 3, IdAeroportoOrigem = 1, NomeAeroportoDestino = "nome aeroporto 2", NomeAeroportoOrigem = "nome aeroporto 1", Valor = 30}
            };

            var retorno = await _rotaOtimizadaService.Object.OtimizarRota(rotas, 3);

            Assert.NotNull(retorno);
            Assert.Equal(30, retorno.CustoTotal);
        }

    }
}