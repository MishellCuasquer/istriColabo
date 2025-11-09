using CatalogoService.Persistence;
using CatalogoService.Services;
using CatalogoService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("Pg")
                 ?? throw new InvalidOperationException("ConnectionStrings:Pg no está configurado.");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogoContext>(opt =>
{
    opt.UseNpgsql(connString);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Generic
builder.Services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));

// Domain-specific
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IStockMovimientoService, StockMovimientoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
