
namespace Necli.LogicaNegocio.Services
{
    [Serializable]
    internal class LogicaNegocioException : Exception
    {
        public LogicaNegocioException()
        {
        }

        public LogicaNegocioException(string? message) : base(message)
        {
        }

        public LogicaNegocioException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}