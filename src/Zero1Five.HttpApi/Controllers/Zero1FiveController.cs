using Zero1Five.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Zero1Five.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class Zero1FiveController : AbpController
    {
        protected Zero1FiveController()
        {
            LocalizationResource = typeof(Zero1FiveResource);
        }
    }
}