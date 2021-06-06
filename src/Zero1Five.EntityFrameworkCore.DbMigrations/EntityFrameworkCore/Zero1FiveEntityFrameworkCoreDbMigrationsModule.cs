using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Zero1Five.EntityFrameworkCore
{
    [DependsOn(
        typeof(Zero1FiveEntityFrameworkCoreModule)
        )]
    public class Zero1FiveEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<Zero1FiveMigrationsDbContext>();
        }
    }
}
