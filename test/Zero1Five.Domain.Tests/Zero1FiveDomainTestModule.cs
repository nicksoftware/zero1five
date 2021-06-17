using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Zero1Five.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five
{
    [DependsOn(
        typeof(Zero1FiveEntityFrameworkCoreTestModule)
        )]
    public class Zero1FiveDomainTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.Replace(ServiceDescriptor.Transient<IGigPictureContainerManager, FakeGigPictureManager>());
            context.Services.Replace(ServiceDescriptor.Transient<IProductPictureManager, FakeProductPictureManager>());
            base.ConfigureServices(context);
        }
    }
}