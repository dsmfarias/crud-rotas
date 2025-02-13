using RotasAereasOtimizadas.Domain.Model;
using RotasAereasOtimizadas.Domain.Repository;

namespace RotasAereasOtimizadas.Domain.Service
{
    public class RotaOtimizadaService
    {
        private readonly IRotaAereaRepository _rotaAereaRepository;

        private RotaOtimizada _rotaOtimizada = new RotaOtimizada();

        public RotaOtimizadaService(IRotaAereaRepository rotaAereaRepository)
        {
            _rotaAereaRepository = rotaAereaRepository;
        }

        public async Task<RotaOtimizada> OtimizarRota(List<RotaAerea> rotasAereas, int idAeroportoDestino)
        {
            List<int> aeroportosAnteriores = new List<int>();
            List<RotaAerea> rotasAtuais = new List<RotaAerea>();
            _rotaOtimizada.CustoTotal = decimal.MaxValue;
            await AvaliarRota(rotasAereas, idAeroportoDestino, aeroportosAnteriores, rotasAtuais);

            return _rotaOtimizada;

        }

        private async Task AvaliarRota(List<RotaAerea> rotasAereas, int idAeroportoDestino, List<int> aeroportosAnteriores, List<RotaAerea> rotasAtuais, decimal valorAtual = 0)
        {
            aeroportosAnteriores.Add(rotasAereas[0].IdAeroportoOrigem);
            foreach (var rota in rotasAereas)
            {
                rotasAtuais.Add(rota);
                decimal valorSomado = valorAtual + rota.Valor;
                if (_rotaOtimizada.CustoTotal > valorSomado)
                {
                    if (rota.IdAeroportoDestino == idAeroportoDestino)
                    {
                        _rotaOtimizada.CustoTotal = valorSomado;
                        _rotaOtimizada.RotasAereas = new List<RotaAerea>(rotasAtuais);
                        continue;
                    }
                    if (!aeroportosAnteriores.Contains(rota.IdAeroportoDestino))
                    {
                        List<RotaAerea> rotasNovoAeroporto = await _rotaAereaRepository.GetRotaAerea(rota.IdAeroportoDestino);
                        await AvaliarRota(rotasNovoAeroporto, idAeroportoDestino, aeroportosAnteriores, rotasAtuais, valorSomado);
                    }
                }
            }
            rotasAtuais.RemoveAt(rotasAtuais.Count - 1);
            aeroportosAnteriores.RemoveAt(aeroportosAnteriores.Count - 1);
            return;
        }
    }
}
