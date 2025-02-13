using RotasAereasOtimizadas.Application.Contracts.Requests;
using RotasAereasOtimizadas.Application.Services;
using RotasAereasOtimizadas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasAereasOtimizadas.Application.Interfaces
{
    /// <summary>
    /// Serviço de rotas aereas
    /// </summary>
    public interface IRotasAereasService
    {
        /// <summary>
        /// Retorna todas as rotas cadastradas
        /// </summary>
        Task<List<RotaAerea>> GetAllRotasAereas();
        /// <summary>
        /// Retorna a rota otimizada
        /// </summary>
        Task<RotaOtimizada> GetRotaOtimizada(int aeroportoOrigem, int aeroportoDestino);
        /// <summary>
        /// Deleta a rota informada
        /// </summary>
        /// <param name="idRota"></param>
        Task<List<RotaAerea>> DeleteRotaAerea(int idRota);
        /// <summary>
        /// Atualiza uma rota cadastrada
        /// </summary>
        /// <param name="rotaRequest"></param>
        /// <returns></returns>
        Task<List<RotaAerea>> PatchRotaAerea(RotaAereaRequest rotaRequest);
        /// <summary>
        /// Adiciona uma nova rota aerea
        /// </summary>
        /// <param name="rotaRequest"></param>
        Task<List<RotaAerea>> AddRotaAerea(RotaAereaRequest rotaRequest);
    }
}
