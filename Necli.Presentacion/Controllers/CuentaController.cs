using Microsoft.AspNetCore.Mvc;
using Necli.Aplicacion.DTOs;
using Necli.LogicaNegocio.Interfaces;

namespace Necli.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpPost]
        public IActionResult CrearCuenta([FromBody] CuentaCrearDTO dto)
        {
            try
            {
                var creada = _cuentaService.CrearCuenta(dto);
                return creada ? StatusCode(201, "Cuenta creada correctamente") : BadRequest("No se pudo crear");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{numero}")]
        public IActionResult ConsultarCuenta(int numero)
        {
            var cuenta = _cuentaService.ConsultarCuenta(numero);
            return cuenta == null ? NotFound("Cuenta no encontrada") : Ok(cuenta);
        }

        [HttpDelete("{numero}")]
        public IActionResult EliminarCuenta(int numero)
        {
            try
            {
                var eliminado = _cuentaService.EliminarCuenta(numero);
                return eliminado ? Ok("Cuenta eliminada") : BadRequest("No se puede eliminar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
