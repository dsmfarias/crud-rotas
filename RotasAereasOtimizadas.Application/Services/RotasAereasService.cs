using RotasAereasOtimizadas.Application.Contracts.Requests;
using RotasAereasOtimizadas.Application.Interfaces;
using RotasAereasOtimizadas.Domain.Model;
using RotasAereasOtimizadas.Domain.Repository;
using RotasAereasOtimizadas.Domain.Service;


namespace RotasAereasOtimizadas.Application.Services
{
    public class RotasAereasService : IRotasAereasService
    {
        private readonly IRotaAereaRepository _rotaAreaRepository;

        private readonly RotaOtimizadaService _rotaOtimizadaService;

        public RotasAereasService(IRotaAereaRepository rotaRepository, RotaOtimizadaService buscarRotaService)
        {
            _rotaAreaRepository = rotaRepository;
            _rotaOtimizadaService = buscarRotaService;
        }

        public async Task<List<RotaAerea>> GetAllRotasAereas()
        {
            return await _rotaAreaRepository.GetAllRotasAereas();
        }

        public async Task<RotaOtimizada> GetRotaOtimizada(int aeroportoOrigem, int aeroportoDestino)
        {
            List<RotaAerea> rotas = await _rotaAreaRepository.GetRotaAerea(aeroportoOrigem);
            return await _rotaOtimizadaService.OtimizarRota(rotas, aeroportoDestino);
        }

        public async Task<List<RotaAerea>> AddRotaAerea(RotaAereaRequest rotaRequest)
        {
            return await _rotaAreaRepository.AddRotaAerea(rotaRequest);
        }

        public async Task<List<RotaAerea>> DeleteRotaAerea(int idRota)
        {
           return await _rotaAreaRepository.DeleteRotaAerea(idRota);
        }

        public async Task<List<RotaAerea>> PatchRotaAerea(RotaAereaRequest rotaRequest)
        {
            return await _rotaAreaRepository.PatchRotaAerea(rotaRequest); 
        }
    }
}
