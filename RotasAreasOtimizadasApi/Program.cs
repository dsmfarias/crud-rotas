using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using RotasAereasOtimizadas.Application.Interfaces;
using RotasAereasOtimizadas.Application.Services;
using RotasAereasOtimizadas.Domain.Repository;
using RotasAereasOtimizadas.Domain.Service;
using RotasAereasOtimizadas.Infrastructure.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Swagger
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD Rotas Aereas Otimizadas", Version = "v1", Description = "CRUD com a escolha de viagem mais barata independente da quantidade de conexões" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Service
builder.Services.AddScoped<IRotasAereasService, RotasAereasService>();
builder.Services.AddScoped<RotaOtimizadaService>();

// Repository
builder.Services.AddScoped<IRotaAereaRepository, RotaAereaRepository>();

// Cache
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0");
    });
}

app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) =>
{
    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    return Results.Problem(exception.Message);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();
