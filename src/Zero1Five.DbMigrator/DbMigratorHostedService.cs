using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zero1Five.Data;
using Serilog;
using Volo.Abp;
using Microsoft.Extensions.Configuration;
using System;

namespace Zero1Five.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<Zero1FiveDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
                options.Services.ReplaceConfiguration(BuildConfiguration());

            }))
            {
                application.Initialize();

                await application
                    .ServiceProvider
                    .GetRequiredService<Zero1FiveDbMigrationService>()
                    .MigrateAsync();

                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        private static IConfiguration BuildConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            // Extra code block to make it possible to read from appsettings.Staging.json
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environmentName == "Staging")
                {
                configurationBuilder.AddJsonFile($"appsettings.{environmentName}.json", true);
                }

            return configurationBuilder
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
