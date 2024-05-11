using Adres.Models;
using Adres.Services.Implementations;
using Adres.Services.Interfaces;
using Adres.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AdresContext>(options => options.UseInMemoryDatabase("AdresDatabase"));
builder.Services.AddScoped<IAdquisicionService,AdquisicionService>();
builder.Services.AddScoped<IParametricaService,ParametricaService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaAdres", app => 
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","Adres API"));
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AdresContext>();
    // Verificar si la tabla paramétrica está vacía
    if (!dbContext.Parametricas.Any())
    {
        // Precargar información en la tabla paramétrica
        dbContext.Parametricas.AddRange(
            new Parametrica { Id = 1, Nombre = "Dirección de Medicamentos y Tecnologías en Salud", Tipo = "Unidad", Codigo="UND001" },
            new Parametrica { Id = 2, Nombre = "Dirección 2", Tipo = "Unidad", Codigo="UND002" },
            new Parametrica { Id = 3, Nombre = "Dirección 3", Tipo = "Unidad", Codigo="UND003" },
            new Parametrica { Id = 4, Nombre = "Dirección 4", Tipo = "Unidad", Codigo="UND004" },
            new Parametrica { Id = 5, Nombre = "Medicamentos", Tipo = "Bien/Servicio", Codigo="BS001" },
            new Parametrica { Id = 6, Nombre = "Medicamentos 2", Tipo = "Bien/Servicio", Codigo="BS002" },
            new Parametrica { Id = 7, Nombre = "servicios", Tipo = "Bien/Servicio", Codigo="BS003" },
            new Parametrica { Id = 8, Nombre = "Servicios 2", Tipo = "Bien/Servicio", Codigo="BS004" },
            new Parametrica { Id = 9, Nombre = "Laboratorios Bayer S.A.", Tipo = "Laboratorio", Codigo="LAB001" },
            new Parametrica { Id = 10, Nombre = "Laboratorios 2", Tipo = "Laboratorio", Codigo="LAB002" },
            new Parametrica { Id = 11, Nombre = "Laboratorios 3", Tipo = "Laboratorio", Codigo="LAB003" },
            new Parametrica { Id = 12, Nombre = "Laboratorios 4", Tipo = "Laboratorio", Codigo="LAB004" }
            );
            // Guardar cambios en la base de datos
            dbContext.SaveChanges();
    }
}     

app.UseCors("PoliticaAdres");
app.Run();
