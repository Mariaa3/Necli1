using Necli.Aplicacion.DTOs;
using Necli.Entidades.Entities;
using Necli.LogicaNegocio.Interfaces;
using Necli.Persistencia.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Linq;

namespace Necli.LogicaNegocio.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CuentaService(ICuentaRepository cuentaRepository, IUsuarioRepository usuarioRepository)
        {
            _cuentaRepository = cuentaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public bool CrearCuenta(CuentaCrearDTO cuentaDto)
        {
            // Validación de edad (mínimo 15 años)
            var edad = DateTime.Today.Year - cuentaDto.Usuario.FechaNacimiento.Year;
            if (cuentaDto.Usuario.FechaNacimiento > DateTime.Today.AddYears(-edad)) edad--;

            if (edad < 15)
            {
                throw new LogicaNegocioException("El usuario debe tener al menos 15 años para crear una cuenta.");
            }

            // Validaciones únicas
            if (_usuarioRepository.ExisteEmail(cuentaDto.Usuario.Email))
                throw new LogicaNegocioException("El correo ya está registrado.");

            if (_usuarioRepository.ExisteTelefono(cuentaDto.Usuario.NumeroTelefono))
                throw new LogicaNegocioException("El número de teléfono ya está registrado.");

            // Mapeo manual
            var usuario = new Usuario { Identificacion = Guid.NewGuid().ToString(),
                Tipo = cuentaDto.Usuario.Tipo,
                Nombres = cuentaDto.Usuario.Nombres, 
                Apellidos = cuentaDto.Usuario.Apellidos,
                Email = cuentaDto.Usuario.Email, 
                NumeroTelefono = cuentaDto.Usuario.NumeroTelefono, ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(cuentaDto.Usuario.Contraseña), FechaCreacion = DateTime.Now };

            var cuenta = new Cuenta
            {
                UsuarioId = usuario.Identificacion,
                Usuario = usuario,
                Saldo = 0,
                FechaCreacion = DateTime.Now
            };

            return _cuentaRepository.CrearCuenta(cuenta);
        }

        public CuentaRespuestaDTO? ConsultarCuenta(int numeroCuenta)
        {
            var cuenta = _cuentaRepository.ConsultarCuenta(numeroCuenta);
            if (cuenta is null)
                return null;

            return new CuentaRespuestaDTO
            {
                Numero = cuenta.Numero,
                UsuarioIdentificacion = cuenta.UsuarioId,
                Titular = cuenta.Usuario?.NombreCompleto ?? "N/A",
                Saldo = cuenta.Saldo,
                FechaCreacion = cuenta.FechaCreacion
            };
        }

        public bool EliminarCuenta(int numeroCuenta)
        {
            var cuenta = _cuentaRepository.ConsultarCuenta(numeroCuenta);
            if (cuenta is null)
                throw new LogicaNegocioException("La cuenta no existe.");

            if (cuenta.Saldo > 50000)
                throw new LogicaNegocioException("No se puede eliminar una cuenta con saldo mayor a $50,000.");

            return _cuentaRepository.EliminarCuenta(numeroCuenta);
        }
    }
}

