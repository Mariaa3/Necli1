using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Necli.Entidades.Entities;

namespace Necli.Persistencia.Interfaces
{
    public interface ITransaccionRepository
    {
        bool Registrar(Transaccion transaccion);
        List<Transaccion> Listar();
    }
}
