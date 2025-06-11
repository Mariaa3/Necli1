using Microsoft.AspNetCore.Mvc;
using Necli.Aplicacion.DTOs;
using Necli.LogicaNegocio.Interfaces;

namespace Necli.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpPost("{cuentaOrigenId}")]
        public IActionResult Realizar(int cuentaOrigenId, [FromBody] TransaccionCrearDTO dto)
        {
            try
            {
                var realizada = _transaccionService.RealizarTransaccion(cuentaOrigenId, dto);
                return realizada ? Ok("Transacción exitosa") : BadRequest("Error al realizar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{cuentaOrigenId}")]
        public IActionResult Consultar(int cuentaOrigenId, [FromQuery] TransaccionConsultaDTO filtro)
        {
            var lista = _transaccionService.ConsultarTransacciones(filtro, cuentaOrigenId);
            return Ok(lista);
        }
    }
}
