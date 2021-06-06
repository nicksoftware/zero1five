using Zero1Five.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Zero1Five.Blazor
{
    public abstract class Zero1FiveComponentBase : AbpComponentBase
    {
        protected Zero1FiveComponentBase()
        {
            LocalizationResource = typeof(Zero1FiveResource);
        }
    }
}
