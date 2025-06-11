using Necli.Aplicacion.DTOs;
using Necli.LogicaNegocio.Interfaces;
using Necli.Persistencia.Interfaces;
using BCrypt = BCrypt.Net.BCrypt;


namespace Necli.LogicaNegocio.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UsuarioRespuestaDTO? Consultar(string identificacion)
        {
            var usuario = _usuarioRepository.Consultar(identificacion);

            if (usuario == null)
                return null;

            return new UsuarioRespuestaDTO
            {
                Identificacion = usuario.Identificacion,
                Tipo = usuario.Tipo,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                NumeroTelefono = usuario.NumeroTelefono,
                FechaCreacion = usuario.FechaCreacion
            };
        }

        public bool Actualizar(string identificacion, UsuarioActualizarDTO dto)
        {
            var usuario = _usuarioRepository.Consultar(identificacion);

            if (usuario == null)
                throw new LogicaNegocioException("El usuario no existe.");

            if (_usuarioRepository.ExisteEmail(dto.Email) && usuario.Email != dto.Email)
                throw new LogicaNegocioException("El correo ya está en uso por otro usuario.");

            usuario.Nombres = dto.Nombres;
            usuario.Apellidos = dto.Apellidos;
            usuario.Email = dto.Email;

            return _usuarioRepository.Actualizar(usuario);
        }

        public bool RestablecerContraseña(RestablecerContraseñaDTO dto)
        {
            var usuario = _usuarioRepository.ConsultarPorEmail(dto.Email);

            if (usuario == null)
                throw new LogicaNegocioException("No existe un usuario con ese correo.");

            var nuevaClave = Guid.NewGuid().ToString().Substring(0, 8);
          



            return _usuarioRepository.Actualizar(usuario);
        }
    }
}
