using Necli.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.LogicaNegocio.Interfaces
{
    public interface ICuentaService
    {
        bool CrearCuenta(CuentaCrearDTO cuentaDto);
        CuentaRespuestaDTO? ConsultarCuenta(int numeroCuenta);
        bool EliminarCuenta(int numeroCuenta);
    }
}
