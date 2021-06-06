using Zero1Five.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Zero1Five.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(Zero1FiveEntityFrameworkCoreDbMigrationsModule),
        typeof(Zero1FiveApplicationContractsModule)
        )]
    public class Zero1FiveDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
