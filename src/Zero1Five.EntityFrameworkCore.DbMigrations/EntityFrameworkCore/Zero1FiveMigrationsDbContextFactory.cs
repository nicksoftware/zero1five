using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Zero1Five.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class Zero1FiveMigrationsDbContextFactory : IDesignTimeDbContextFactory<Zero1FiveMigrationsDbContext>
    {
        public Zero1FiveMigrationsDbContext CreateDbContext(string[] args)
        {
            Zero1FiveEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<Zero1FiveMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new Zero1FiveMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zero1Five.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
