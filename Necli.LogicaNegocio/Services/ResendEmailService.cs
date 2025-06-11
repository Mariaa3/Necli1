using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Necli.LogicaNegocio.Interfaces;

namespace Necli.LogicaNegocio.Services
{
    public class ResendEmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ResendEmailService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Resend:ApiKey"]!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<bool> EnviarCorreo(string destinatario, string asunto, string contenido)
        {
            var data = new
            {
                from = "Necli <info@necli.app>",
                to = new[] { destinatario },
                subject = asunto,
                html = $"<strong>{contenido}</strong>"
            };

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.resend.com/emails", content);
            return response.IsSuccessStatusCode;
        }
    }
}
