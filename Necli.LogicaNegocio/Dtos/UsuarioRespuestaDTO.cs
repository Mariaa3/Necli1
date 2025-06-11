using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Aplicacion.DTOs
{
    public class UsuarioRespuestaDTO
    {
        public string Identificacion { get; set; }

        public string Tipo { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Email { get; set; }

        public string NumeroTelefono { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}

