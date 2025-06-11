using Microsoft.AspNetCore.Mvc;
using Necli.LogicaNegocio.Interfaces;
using Necli.LogicaNegocio.Services;
using Necli.Persistencia.Interfaces;
using System.Linq;

namespace Necli.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly PdfService _pdfService;
        private readonly IEmailService _emailService;

        public ReportesController(
            IUsuarioRepository usuarioRepository,
            ITransaccionRepository transaccionRepository,
            PdfService pdfService,
            IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _transaccionRepository = transaccionRepository;
            _pdfService = pdfService;
            _emailService = emailService;
        }

        [HttpPost("enviar-reporte")]
        public async Task<IActionResult> EnviarReporteMensual([FromQuery] string identificacion, [FromQuery] int mes, [FromQuery] int año)
        {
            var usuario = _usuarioRepository.Consultar(identificacion);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            var transacciones = _transaccionRepository.Listar()
                .Where(t =>
                    (t.CuentaOrigen?.UsuarioId == identificacion || t.CuentaDestino?.UsuarioId == identificacion) &&
                    t.Fecha.Month == mes && t.Fecha.Year == año)
                .ToList();

            if (!transacciones.Any())
                return NotFound("No hay transacciones para ese mes");

            var contenido = $"Reporte mensual de {usuario.NombreCompleto}\n\n";
            foreach (var t in transacciones)
            {
                contenido += $"{t.Fecha:dd/MM/yyyy} - {t.Tipo.ToUpper()} - ${t.Monto:N0}\n";
            }

            string nombreArchivo = usuario.Identificacion + "_reporte";
            string ruta = _pdfService.GenerarPdfReporte(contenido, usuario.Identificacion, mes, año, nombreArchivo);

            string mensaje = $"Hola {usuario.Nombres}, tu reporte mensual ha sido generado correctamente y puedes visualizarlo en el sistema.";

            await _emailService.EnviarCorreo(usuario.Email, "Reporte mensual generado", mensaje);
            return Ok("Reporte generado y correo enviado.");
        }
    }
}
