using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Necli.LogicaNegocio.Interfaces
{
    public interface IEmailService
    {
        Task<bool> EnviarCorreo(string destinatario, string asunto, string contenido);
    }
}
