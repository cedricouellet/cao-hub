using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CaoHub.Data.Design
{
    internal class DesignTimeCaoHubDbContextFactory() : IDesignTimeDbContextFactory<CaoHubDbContext>
    {
        public CaoHubDbContext CreateDbContext(string[] args)
        {
            // Looks for a .env file where the connection credentials and port are stored.
            Env.Load(path: null, Env.TraversePath());

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CaoHubDbContext")!
              .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT")!)
              .Replace("${DB_ADMIN}", Environment.GetEnvironmentVariable("DB_ADMIN")!)
              .Replace("${DB_ADMIN_PASSWORD}", Environment.GetEnvironmentVariable("DB_ADMIN_PASSWORD")!);

            var optionsBuilder = new DbContextOptionsBuilder<CaoHubDbContext>()
                .UseSqlServer(connectionString);

            return new CaoHubDbContext(optionsBuilder.Options);
        }
    }
}
