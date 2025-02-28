using CaoHub.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

// Load nearest .env file into environment variables.
Env.Load(path: null, Env.TraversePath());

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<CaoHubDbContext>(o =>
{
    var connectionString = configuration.GetConnectionString("CaoHubDbContext")!
        .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
        .Replace("${DB_USER_PASSWORD}", Environment.GetEnvironmentVariable("DB_USER_PASSWORD"));

    o.UseSqlServer(connectionString);
    o.EnableDetailedErrors(!builder.Environment.IsProduction());
    o.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsProduction()) 
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
