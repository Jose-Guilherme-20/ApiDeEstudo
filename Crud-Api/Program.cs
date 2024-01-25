using System;
using System.Data.Common;
using Crud_Api.Context;
using Crud_Api.Repository;
using Crud_Api.Repository.Interfaces;
using Crud_Api.Services;
using Crud_Api.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));

var connection = builder.Configuration["ConnectionStrings:Connection"];
builder.Services.AddDbContext<EntityContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(5, 0, 0))));
builder.Services.AddSingleton<DbConnection>(conn => new MySqlConnection(connection));
builder.Services.AddScoped<DapperContext>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nome do seu API", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins("http://localhost:3000") // Coloque a origem do seu frontend
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials() // Adicione isso se você estiver usando credenciais (cookies, cabeçalhos de autenticação, etc.)
    );
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nome do seu API v1");
    // A linha abaixo serve para acessar a documentação Swagger na raiz do seu aplicativo.
    // Certifique-se de que isso não entra em conflito com outras rotas em seu aplicativo.
    c.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();