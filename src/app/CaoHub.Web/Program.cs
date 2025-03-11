using CaoHub.Web.Areas.ReceiptManagement.Extensions;
using CaoHub.Web.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Tailwind;

// Looks for a .env file where the connection credentials and port are stored.
Env.Load(path: null, Env.TraversePath());

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<CaoHubDbContext>(options =>
{
    var connectionString = configuration.GetConnectionString("CaoHubDbContext_User")!
        .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
        .Replace("${DB_USER_PASSWORD}", Environment.GetEnvironmentVariable("DB_USER_PASSWORD"));

    options.UseSqlServer(connectionString);
    options.EnableDetailedErrors(builder.Environment.IsDevelopment());
});

services.AddReceiptManagementServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    // Start tailwind watch worker
    _ = app.RunTailwind("css:watch");
}

app.Run();
