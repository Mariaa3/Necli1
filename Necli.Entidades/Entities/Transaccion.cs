using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Necli.Entidades.Entities
{
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Numero { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int CuentaOrigenId { get; set; }

        [ForeignKey("CuentaOrigenId")]
        public Cuenta? CuentaOrigen { get; set; }

        [Required]
        public int CuentaDestinoId { get; set; }

        [ForeignKey("CuentaDestinoId")]
        public Cuenta? CuentaDestino { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public required string Tipo { get; set; }
    }
}
