using Microsoft.AspNetCore.Mvc;
using RotasAereasOtimizadas.Application.Contracts.Requests;
using RotasAereasOtimizadas.Application.Interfaces;


namespace RotasAreasOtimizadas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotasAereasController : ControllerBase
    {
        private IRotasAereasService _rotasAereasService;
        public RotasAereasController(IRotasAereasService rotasAereasService)
        {
            _rotasAereasService = rotasAereasService;
        }

        /// <summary>
        /// Lista as rotas aereas cadastradas.
        /// </summary>
        /// <returns>todas rotas cadastradas</returns>
        /// <response code="200">Retorna todas as rotas cadastradas</response>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _rotasAereasService.GetAllRotasAereas();
            return new OkObjectResult(response);
        }

        /// <summary>
        /// Retorna a rota otimizada por valor entre os aeroportos
        /// </summary>
        /// <returns>Rota otimizada/returns>
        /// <response code="200">Retorna a rota otimizada</response>
        [HttpGet("Rota-Otimizada")]
        public async Task<ActionResult> GetMelhorRota([FromQuery] int aeroportoOrigem, [FromQuery] int aeroportoDestino)
        {
            var response = await _rotasAereasService.GetRotaOtimizada(aeroportoOrigem, aeroportoDestino);
            return new OkObjectResult(response);
        }


        /// <summary>
        /// Cadastra nova rota aerea.
        /// </summary>
        /// <returns>rota cadastrada</returns>
        /// <response code="200">Retorna uma lista atualizada de todas as rotas</response>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RotaAereaRequest request)
        {
            var response = await _rotasAereasService.AddRotaAerea(request);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// Atualiza uma rota ja cadastrada
        /// </summary>
        /// <returns>rotas atualizada</returns>
        /// <response code="200">Retorna uma lista atualizada de todas as rotas</response>
        [HttpPatch]
        public async Task<ActionResult> Patch([FromBody] RotaAereaRequest request)
        {
            var response = await _rotasAereasService.PatchRotaAerea(request);
            return new OkObjectResult(response);

        }

        /// <summary>
        /// Deleta uma rota existente.
        /// </summary>
        /// <returns>rotas cadastradas</returns>
        /// <response code="200">Retorna uma lista atualizada de todas as rotas</response>
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int idRota)
        {
            var response = await _rotasAereasService.DeleteRotaAerea(idRota);

            return new OkObjectResult(response);

        }
    }
}
