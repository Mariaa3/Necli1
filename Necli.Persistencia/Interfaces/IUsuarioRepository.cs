using Necli.Entidades.Entities;
using System.Collections.Generic;

namespace Necli.Persistencia.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario? Consultar(string identificacion);
        Usuario? ConsultarPorEmail(string email);
        bool Actualizar(Usuario usuario);
        bool ExisteEmail(string email);
        bool ExisteTelefono(string telefono);
        List<Usuario> ObtenerTodos(); 
    }
}

