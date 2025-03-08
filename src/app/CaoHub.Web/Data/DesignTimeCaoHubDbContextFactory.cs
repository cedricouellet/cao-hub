using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CaoHub.Web.Data
{
    /// <summary>
    /// The factory for creating CAOHub DbContexts at design time. 
    /// </summary>
    internal class DesignTimeCaoHubDbContextFactory() : IDesignTimeDbContextFactory<CaoHubDbContext>
    {
        /// <summary>
        /// Creates an instance of a <see cref="CaoHubDbContext"/>
        /// </summary>
        /// <param name="args">The arguments to pass to the factory.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public CaoHubDbContext CreateDbContext(string[] args)
        {
            // Looks for a .env file where the connection credentials and port are stored.
            Env.Load(path: null, Env.TraversePath());

            string admin = Environment.GetEnvironmentVariable("DB_ADMIN")
                ?? throw new InvalidOperationException("DB_ADMIN must be set in .env");

            string? adminPassword = Environment.GetEnvironmentVariable("DB_ADMIN_PASSWORD")
                ?? throw new InvalidOperationException("DB_ADMIN_PASSWORD must be set in .env");

            int port = Convert.ToInt32(Environment.GetEnvironmentVariable("DB_PORT")
                ?? throw new InvalidOperationException("DB_PORT must be set in .env"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CaoHubDbContext_Admin")!
              .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
              .Replace("${DB_ADMIN}", Environment.GetEnvironmentVariable("DB_ADMIN"))
              .Replace("${DB_ADMIN_PASSWORD}", Environment.GetEnvironmentVariable("DB_ADMIN_PASSWORD"));

            var optionsBuilder = new DbContextOptionsBuilder<CaoHubDbContext>()
                .UseSqlServer(connectionString);

            return new CaoHubDbContext(optionsBuilder.Options);
        }
    }
}
