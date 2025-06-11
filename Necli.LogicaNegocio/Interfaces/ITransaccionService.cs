using Necli.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.LogicaNegocio.Interfaces
{
    public interface ITransaccionService
    {
        bool RealizarTransaccion(int cuentaOrigenId, TransaccionCrearDTO dto);
        List<TransaccionRespuestaDTO> ConsultarTransacciones(TransaccionConsultaDTO filtro, int cuentaOrigenId);
    }
}
