using Microsoft.Extensions.Hosting;
using Necli.Entidades.Entities;
using Necli.LogicaNegocio.Interfaces;
using Necli.Persistencia.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Necli.LogicaNegocio.Services
{
    public class EmailSchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailSchedulerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var usuarioRepo = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var pdfService = scope.ServiceProvider.GetRequiredService<PdfService>();

                    var usuarios = usuarioRepo.ObtenerTodos();

                    foreach (var usuario in usuarios)
                    {
                        string contenido = $"Reporte mensual para {usuario.NombreCompleto}\n\nGenerado automáticamente por NECLI.";

                        string ruta = pdfService.GenerarPdfReporte(
                            contenido,
                            usuario.Identificacion,
                            DateTime.Now.Month,
                            DateTime.Now.Year,
                            usuario.Identificacion + "_reporte");

                        string mensaje = $"Hola {usuario.Nombres}, tu reporte mensual ha sido generado y puedes visualizarlo en el sistema. Tu contraseña es tu número de cédula.";

                        await emailService.EnviarCorreo(usuario.Email, "Reporte mensual generado", mensaje);
                    }
                }

                // Espera 30 días (puedes cambiar a minutos para pruebas)
                await Task.Delay(TimeSpan.FromDays(30), stoppingToken);
            }
        }
    }
}
