using System;
using System.ComponentModel.DataAnnotations;

namespace Necli.Entidades.Entities
{
    public class Usuario
    {
        [Key]
        public string Identificacion { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string NumeroTelefono { get; set; } = string.Empty;

        [Required]
        public string ContraseñaHash { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

    
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        public string Correo => Email;
    }
}
