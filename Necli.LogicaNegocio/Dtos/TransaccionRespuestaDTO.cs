using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Aplicacion.DTOs
{
    public class TransaccionRespuestaDTO
    {
        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public int CuentaOrigenId { get; set; }

        public int CuentaDestinoId { get; set; }

        public decimal Monto { get; set; }

        public string Tipo { get; set; }
    }
}
