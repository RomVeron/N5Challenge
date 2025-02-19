using Core.Entities;
using Core.Interfaces;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using Nest;
using Serilog;
using WebApi.Infraestructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar Elasticsearch
builder.Services.AddSingleton<IElasticClient>(sp =>
{
	var settings = new ConnectionSettings(new Uri(builder.Configuration["Elasticsearch:Uri"]))
		.DefaultIndex("permissions") // Índice por defecto
		.DisableDirectStreaming()
		.PrettyJson();

	return new ElasticClient(settings);
});

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.Enrich.WithThreadId()
	.WriteTo.Console()
	.CreateLogger();

builder.Host.UseSerilog(); // Inyecta Serilog en la aplicación

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<Core.Interfaces.IRepository<PermissionType>, Repository<PermissionType>>();
builder.Services.AddScoped<IElasticSearchService, ElasticSearchService>();
builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();


builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configurar middleware de logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
