using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Aplicacion.DTOs
{
    public class TransaccionInterbancariaDTO
    {
        public string TipoDocumentoTitular { get; set; } = string.Empty;

        public string NumeroDocumentoTitular { get; set; } = string.Empty;

        public string NumeroCuenta { get; set; } = string.Empty;

        public decimal Monto { get; set; }

        public string Moneda { get; set; } = "COP";

        public string BancoDestino { get; set; } = string.Empty;
    }
}

