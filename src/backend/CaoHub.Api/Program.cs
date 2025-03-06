using CaoHub.Api.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

// Load .env file
Env.Load(path: null, Env.TraversePath());

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors(config =>
{
    if (builder.Environment.IsDevelopment())
    {
        config.AddDefaultPolicy(
            p => p.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
    }
});

var mvcBuilder = services.AddControllers();
mvcBuilder.AddJsonOptions(config =>
{
    config.JsonSerializerOptions.AllowTrailingCommas = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

services.Configure<RouteOptions>(config =>
{
    config.LowercaseQueryStrings = true;
    config.LowercaseUrls = true;
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<CaoHubDbContext>(options =>
{
    var connectionString = configuration.GetConnectionString("CaoHubDbContext_User")!
        .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
        .Replace("${DB_USER_PASSWORD}", Environment.GetEnvironmentVariable("DB_USER_PASSWORD"));

    options.UseSqlServer(connectionString);
    options.EnableDetailedErrors(builder.Environment.IsDevelopment());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
