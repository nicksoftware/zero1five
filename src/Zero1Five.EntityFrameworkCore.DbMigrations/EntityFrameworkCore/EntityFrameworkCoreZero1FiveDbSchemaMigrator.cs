using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zero1Five.Data;
using Volo.Abp.DependencyInjection;

namespace Zero1Five.EntityFrameworkCore
{
    public class EntityFrameworkCoreZero1FiveDbSchemaMigrator
        : IZero1FiveDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreZero1FiveDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the Zero1FiveMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<Zero1FiveMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}