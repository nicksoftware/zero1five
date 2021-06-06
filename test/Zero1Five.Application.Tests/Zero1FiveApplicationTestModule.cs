using Volo.Abp.Modularity;

namespace Zero1Five
{
    [DependsOn(
        typeof(Zero1FiveApplicationModule),
        typeof(Zero1FiveDomainTestModule)
        )]
    public class Zero1FiveApplicationTestModule : AbpModule
    {

    }
}