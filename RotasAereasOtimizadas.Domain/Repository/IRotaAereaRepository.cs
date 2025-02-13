using RotasAereasOtimizadas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasAereasOtimizadas.Domain.Repository
{
    /// <summary>
    /// Repository CRUD rotas cadastrasdas
    /// </summary>
    public interface IRotaAereaRepository
    {
        Task<List<RotaAerea>> GetAllRotasAereas();
        Task<List<RotaAerea>> GetRotaAerea(int aeportoId);
        Task<List<RotaAerea>> AddRotaAerea(RotaAerea rotaAerea);
        Task<List<RotaAerea>> PatchRotaAerea(RotaAerea rotaAerea);
        Task<List<RotaAerea>> DeleteRotaAerea(int idRotaAerea);
    }
}
