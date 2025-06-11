using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Aplicacion.DTOs
{
    public class TransaccionCrearDTO
    {
        public int CuentaDestinoId { get; set; }

        public decimal Monto { get; set; }

        public string Tipo { get; set; } = string.Empty; // "entrada" o "salida"
    }
}
