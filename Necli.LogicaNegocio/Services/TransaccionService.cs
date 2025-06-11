using Necli.Aplicacion.DTOs;
using Necli.Entidades.Entities;
using Necli.LogicaNegocio.Interfaces;
using Necli.Persistencia.Interfaces;
using System;
using System.Linq;

namespace Necli.LogicaNegocio.Services
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly ICuentaRepository _cuentaRepository;

        public TransaccionService(ITransaccionRepository transaccionRepository, ICuentaRepository cuentaRepository)
        {
            _transaccionRepository = transaccionRepository;
            _cuentaRepository = cuentaRepository;
        }

        public bool RealizarTransaccion(int cuentaOrigenId, TransaccionCrearDTO dto)
        {
            if (cuentaOrigenId == dto.CuentaDestinoId)
                throw new LogicaNegocioException("No se puede transferir a la misma cuenta.");

            if (dto.Monto < 1000 || dto.Monto > 5000000)
                throw new LogicaNegocioException("El monto debe estar entre $1.000 y $5.000.000.");

            var origen = _cuentaRepository.ConsultarCuenta(cuentaOrigenId);
            var destino = _cuentaRepository.ConsultarCuenta(dto.CuentaDestinoId);

            if (origen == null || destino == null)
                throw new LogicaNegocioException("Cuenta origen o destino no válida.");

            if (origen.Saldo < dto.Monto)
                throw new LogicaNegocioException("Saldo insuficiente.");

            // Transferencia
            origen.Saldo -= dto.Monto;
            destino.Saldo += dto.Monto;

            var transaccion = new Transaccion
            {
                CuentaOrigenId = cuentaOrigenId,
                CuentaDestinoId = dto.CuentaDestinoId,
                Monto = dto.Monto,
                Tipo = dto.Tipo,
                Fecha = DateTime.Now
            };

            _cuentaRepository.Actualizar(origen);
            _cuentaRepository.Actualizar(destino);

            return _transaccionRepository.Registrar(transaccion);
        }

        public List<TransaccionRespuestaDTO> ConsultarTransacciones(TransaccionConsultaDTO filtro, int cuentaOrigenId)
        {
            var transacciones = _transaccionRepository.Listar()
                .Where(t => t.CuentaOrigenId == cuentaOrigenId)
                .ToList();

            if (filtro.FechaDesde.HasValue)
            {
                transacciones = transacciones
                    .Where(t => t.Fecha.Date >= filtro.FechaDesde.Value.Date)
                    .ToList();
            }

            if (filtro.FechaHasta.HasValue)
            {
                transacciones = transacciones
                    .Where(t => t.Fecha.Date <= filtro.FechaHasta.Value.Date)
                    .ToList();
            }

            if (filtro.CuentaDestino.HasValue)
            {
                transacciones = transacciones
                    .Where(t => t.CuentaDestinoId == filtro.CuentaDestino)
                    .ToList();
            }

            return transacciones.Select(t => new TransaccionRespuestaDTO
            {
                Numero = t.Numero,
                Fecha = t.Fecha,
                CuentaOrigenId = t.CuentaOrigenId,
                CuentaDestinoId = t.CuentaDestinoId,
                Monto = t.Monto,
                Tipo = t.Tipo
            }).ToList();
        }
    }
}
