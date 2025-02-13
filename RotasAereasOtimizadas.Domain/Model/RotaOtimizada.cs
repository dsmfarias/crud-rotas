using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasAereasOtimizadas.Domain.Model
{
    public class RotaOtimizada
    {
        public List<RotaAerea> RotasAereas { get; set; }
        public decimal CustoTotal { get; set; }
    }
}
