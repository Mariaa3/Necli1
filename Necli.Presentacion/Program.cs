using Necli.LogicaNegocio.Interfaces;
using Necli.LogicaNegocio.Services;
using Necli.Persistencia.Interfaces;
using Necli.Persistencia.Repositorios;
using Microsoft.OpenApi.Models;
using Necli.LogicaNegocio.Perfiles;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NECLI API", Version = "v1" });
});

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UsuarioProfile>();
});

// ADO.NET Repositorios con tu cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IUsuarioRepository>(_ => new UsuarioRepository(connectionString));
builder.Services.AddScoped<ICuentaRepository>(_ => new CuentaRepository(connectionString));
builder.Services.AddScoped<ITransaccionRepository>(_ => new TransaccionRepository(connectionString));

// Servicios de aplicación
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();

// Correo con Resend (usa HttpClient)
builder.Services.AddHttpClient<IEmailService, ResendEmailService>();

// PDF mensual
builder.Services.AddScoped<PdfService>();

// Programación automática
builder.Services.AddHostedService<EmailSchedulerService>();


builder.Services.AddControllers();
WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

