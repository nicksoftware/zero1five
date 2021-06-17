using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;
using Zero1Five.AzureStorage.Products;
using Zero1Five.Products;

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