using ContactosAPI.Models;
using ContactosAPI.Servicio;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//agrego el servicio de conexion a la base de datos
builder.Services.Configure<ContactsDatabaseSettings>(
    builder.Configuration.GetSection("ContactsDatabase"));
builder.Services.AddSingleton<ContactsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//usa el Middleware Para agregar los errores Predeterminados 404
app.UseStatusCodePages();

// aqui personalizo el middleware para la api
app.Use(async (context, next) =>
{
    await next();
    if (context.Request.Path != "/api/Contactos")
    {
        context.Response.StatusCode = 404;
        await next();
    }
});

app.Run();
