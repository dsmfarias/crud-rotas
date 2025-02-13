using RotasAereasOtimizadas.Domain.Model;
using RotasAereasOtimizadas.Domain.Repository;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace RotasAereasOtimizadas.Infrastructure.Repository
{
    public class RotaAereaRepository : IRotaAereaRepository
    {

        private readonly IMemoryCache _memoryCache;

        public RotaAereaRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<RotaAerea>> GetAllRotasAereas()
        {
            if (!_memoryCache.TryGetValue("rotas", out List<RotaAerea> rotas))
            {
                string rotasJson = await File.ReadAllTextAsync("RotaRepository.json");

                rotas = JsonSerializer.Deserialize<List<RotaAerea>>(rotasJson);
                _memoryCache.Set("rotas", rotas);
            }
            return rotas;
        }

        public async Task<List<RotaAerea>> GetRotaAerea(int idAeroporto)
        {
            List<RotaAerea> rotasAreas = await GetAllRotasAereas();
            rotasAreas = rotasAreas.FindAll(rota => rota.IdAeroportoOrigem == idAeroporto);
            return rotasAreas;
        }

        public async Task<List<RotaAerea>> AddRotaAerea(RotaAerea rotaAerea)
        {
            List<RotaAerea> rotas = await GetAllRotasAereas();

            rotaAerea.Id = rotas[rotas.Count - 1].Id + 1;
            rotas.Add(rotaAerea);

            File.WriteAllText("RotaRepository.json", JsonSerializer.Serialize(rotas));
            _memoryCache.Set("rotas", rotas);

            return rotas;
        }

        public async Task<List<RotaAerea>> PatchRotaAerea(RotaAerea rotaAerea)
        {
            List<RotaAerea> rotas = await GetAllRotasAereas();
            int indexRota = rotas.FindIndex(r => r.Id == rotaAerea.Id);
            if (indexRota == -1)
            {
                throw new Exception("Rota Aerea não cadastrada");
            }
            rotas[indexRota] = rotaAerea;
            File.WriteAllText("RotaRepository.json", JsonSerializer.Serialize(rotas));
            _memoryCache.Set("rotas", rotas);
            return rotas;
        }
        public async Task<List<RotaAerea>> DeleteRotaAerea(int idRotaAerea)
        {
            List<RotaAerea> rotas = await GetAllRotasAereas();
            int indexParaRemover = rotas.FindIndex(rota => rota.Id == idRotaAerea);
            if (indexParaRemover == -1)
            {
                throw new Exception("Rota Aerea não cadastrada");
            }
            rotas.RemoveAt(indexParaRemover);
            File.WriteAllText("RotaRepository.json", JsonSerializer.Serialize(rotas));
            _memoryCache.Set("rotas", rotas);
            return rotas;
        }
    }
}
