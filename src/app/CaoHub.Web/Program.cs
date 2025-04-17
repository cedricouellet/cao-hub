using CaoHub.Web.Areas.Facturio.Extensions;
using CaoHub.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Tailwind;
using Microsoft.AspNetCore.Localization;
using CaoHub.Web.Resources;

// Looks for a .env file where the connection credentials and port are stored.
Env.Load(path: null, Env.TraversePath());

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddRouting(options =>
{
    options.AppendTrailingSlash = false;
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

services.AddLocalization();

services.AddRequestLocalization(options =>
{
    var supportedCultures = configuration["Localization:SupportedCultures"]!.Split(';');

    options.SetDefaultCulture(supportedCultures[0]);
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);
    options.RequestCultureProviders = 
    [
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider(),
        new QueryStringRequestCultureProvider(),
    ];
});

services.AddControllersWithViews()
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                return factory.Create(typeof(SharedResource));
            };
        });

services.AddDbContext<CaoHubDbContext>(options =>
{
    var connectionString = configuration.GetConnectionString("CaoHubDbContext")!
        .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
        .Replace("${DB_USER_PASSWORD}", Environment.GetEnvironmentVariable("DB_USER_PASSWORD"));

    options.UseSqlServer(connectionString);
    options.EnableDetailedErrors(builder.Environment.IsDevelopment());
});

services.AddFacturioServices();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/error/{0}");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/500");
    app.UseHsts();
}   
else
{
    app.UseDeveloperExceptionPage();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("Areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    // Start tailwind watch worker
    _ = app.RunTailwind("css:watch");
}

app.Run();
