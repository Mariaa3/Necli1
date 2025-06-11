using Necli.Aplicacion.DTOs;

namespace Necli.LogicaNegocio.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioRespuestaDTO? Consultar(string identificacion);
        bool Actualizar(string identificacion, UsuarioActualizarDTO dto);
        bool RestablecerContraseña(RestablecerContraseñaDTO dto);
    }
}
