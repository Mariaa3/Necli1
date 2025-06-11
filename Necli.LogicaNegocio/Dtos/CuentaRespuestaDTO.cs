using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Aplicacion.DTOs
{
    public class CuentaRespuestaDTO
    {
        public int Numero { get; set; }

        public string UsuarioIdentificacion { get; set; }

        public string Titular { get; set; }

        public decimal Saldo { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
