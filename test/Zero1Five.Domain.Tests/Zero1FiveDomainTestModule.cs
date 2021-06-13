using Zero1Five.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Zero1Five
{
    [DependsOn(
        typeof(Zero1FiveEntityFrameworkCoreTestModule)
        )]
    public class Zero1FiveDomainTestModule : AbpModule
    {
        
    }
}