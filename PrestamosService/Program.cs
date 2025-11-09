using CatalogoService.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PrestamosService.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ---------- Configuración base ----------
var connString = builder.Configuration.GetConnectionString("Pg")
                 ?? throw new InvalidOperationException("ConnectionStrings:Pg no está configurado.");

// Ajuste de compatibilidad de fechas (opcional)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ---------- Servicios ----------
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (usa esquema prestamos por defecto)
builder.Services.AddDbContext<PrestamosContext>(opt =>
{
    opt.UseNpgsql(connString);
});

// Health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(connString, name: "postgres");

// CORS (libre para pruebas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Si luego agregas servicios de dominio, los registras aquí:
// builder.Services.AddScoped<IPrestamosService, PrestamosServiceImpl>();

var app = builder.Build();

// ---------- Middleware / Pipeline ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PrestamosService v1");
        options.RoutePrefix = string.Empty; // Esto hace que Swagger se abra en "/"
    });
}

// HTTPS y CORS
app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Seguridad básica
app.UseAuthorization();

// Controladores
app.MapControllers();

// HealthCheck (útil en despliegues Docker)
app.MapHealthChecks("/health");

// ---------- Migraciones automáticas (opcional pero útil) ----------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PrestamosContext>();
    db.Database.Migrate();
}

app.Run();
