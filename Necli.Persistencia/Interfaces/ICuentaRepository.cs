using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Necli.Entidades.Entities;

namespace Necli.Persistencia.Interfaces
{
    public interface ICuentaRepository
    {
        bool CrearCuenta(Cuenta cuenta);
        Cuenta? ConsultarCuenta(int numeroCuenta);
        bool EliminarCuenta(int numeroCuenta);
        bool Actualizar(Cuenta cuenta);
    }
}
