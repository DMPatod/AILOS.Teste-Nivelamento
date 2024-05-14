using Dapper;
using MediatR;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

builder.Services.AddTransient<IBaseCommandRepository<ContaCorrente>, ContaCorrenteCommandRepository>();
builder.Services.AddTransient<IBaseCommandRepository<Idempotencia>, IdempotenciaCommandRepository>();
builder.Services.AddTransient<IBaseCommandRepository<Movimento>, MovimentoCommandRepository>();

builder.Services.AddTransient<IContaCorrenteQueryRepository, ContaCorrenteQueryRepository>();
builder.Services.AddTransient<IBaseQueryRepository<Idempotencia>, IdempotenciaQueryRepository>();
builder.Services.AddTransient<IMovimentoQueryRepository, MovimentoQueryRepository>();

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

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


