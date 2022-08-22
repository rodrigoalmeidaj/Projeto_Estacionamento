using API_Estacionamento.AppDbContext;
using API_Estacionamento.Controllers;
using API_Estacionamento.Interfaces;
using API_Estacionamento.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Estacionamento",
        Description = "Uma API Web ASP.NET Core para gerenciar itens de um estacionamento",
        //Contact = new OpenApiContact
        //{
        //    Name = "http://servidor4.ddnsfree.com:5004/api/.....",
        //    Url = new Uri("http://servidor4.ddnsfree.com:5004")
        //},
        //License = new OpenApiLicense
        //{
        //    Name = "Example License",
        //    Url = new Uri("https://example.com/license")
        //}
    });


    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Comunicação com o banco Postgres
builder.Services.AddDbContext<PostgresContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Injeção de dependencia
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IPagamentoService, PagamentoService>();
builder.Services.AddTransient<IClienteService, ClienteServico>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {  
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //options.RoutePrefix = "Api/Documentacao";
    });
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "Api/Documentacao";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
