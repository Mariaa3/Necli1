using Microsoft.AspNetCore.Mvc;
using Necli.Aplicacion.DTOs;
using Necli.LogicaNegocio.Interfaces;

namespace Necli.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{identificacion}")]
        public IActionResult Consultar(string identificacion)
        {
            var usuario = _usuarioService.Consultar(identificacion);
            return usuario == null ? NotFound("Usuario no encontrado") : Ok(usuario);
        }

        [HttpPut("{identificacion}")]
        public IActionResult Actualizar(string identificacion, [FromBody] UsuarioActualizarDTO dto)
        {
            try
            {
                var actualizado = _usuarioService.Actualizar(identificacion, dto);
                return actualizado ? Ok("Usuario actualizado") : NotFound("No se actualizó");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

