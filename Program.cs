using FastEndpoints;
using FluentMigrator.Runner;
using Hangfire;
using Microsoft.OpenApi.Models;
using MinimalApi_test____Dapper___PostgreSQL.BackgroundJobServicess;
using MinimalApi_test____Dapper___PostgreSQL.DataBase;
using MinimalApi_test____Dapper___PostgreSQL.Endpoints;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using MinimalApi_test____Dapper___PostgreSQL.Middlewares;
using MinimalApi_test____Dapper___PostgreSQL.Migrations;
using MinimalApi_test____Dapper___PostgreSQL.Services;
using Serilog;
using System.Data;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyMinimalApi", Version = "v1" });
});
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<IDbConnectionFactory>
(
 serviceProvider => new SqlFactory()
);
builder.Services.AddTransient<IToDolister, ToDoListService>();
builder.Services.AddTransient<IDateTime, DateTimeService>();
builder.Services.AddHangfire(h => h.UseSqlServerStorage("Server=DESKTOP-S9AIDDH\\SQLEXPRESS; Database=FluentMigratorDb; Trusted_Connection=True; TrustServerCertificate=True"));
builder.Services.AddHangfireServer();
builder.Services.AddSingleton<BackGroundJobService>();
builder.Services.AddTransient<IBackgroundJobClient, BackgroundJobClient>();
builder.Services.AddMemoryCache();
Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()         
           .CreateLogger();
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var serviceProvider = new ServiceCollection()
           .AddFluentMigratorCore()
           .ConfigureRunner(rb => rb
           .AddSqlServer()
           .WithGlobalConnectionString("Server=DESKTOP-S9AIDDH\\SQLEXPRESS; Database=FluentMigratorDb; Trusted_Connection=True; TrustServerCertificate=True")
           .ScanIn(typeof(CreateToDoTable).Assembly).For.Migrations())
           .AddLogging(lb => lb.AddFluentMigratorConsole())
           .BuildServiceProvider();
//var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
//runner.MigrateUp();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CancellationMiddleware>();
var backgroundService = app.Services.GetRequiredService<BackGroundJobService>();
backgroundService.CheckScheduledTasks();
app.UseHangfireDashboard("/dashboard");
app.UseSwagger(); 
app.UseSwaggerUI();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseHttpsRedirection();
app.Run();


