using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasAereasOtimizadas.Domain.Model
{
    public class RotaAerea
    {
        public int? Id { get; set; }

        public int IdAeroportoOrigem { get; set; }

        public string? NomeAeroportoOrigem { get; set; }

        public int IdAeroportoDestino { get; set; }

        public string? NomeAeroportoDestino { get; set; }

        public decimal Valor { get; set; }

    }
}
