using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Aplicacion.DTOs
{
    public class UsuarioCrearDTO
    {
        public string Tipo { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NumeroTelefono { get; set; } = string.Empty;

        public string Contraseña { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }
    }
}
