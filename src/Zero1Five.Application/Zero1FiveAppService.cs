using System;
using System.Collections.Generic;
using System.Text;
using Zero1Five.Localization;
using Volo.Abp.Application.Services;

namespace Zero1Five
{
    /* Inherit your application services from this class.
     */
    public abstract class Zero1FiveAppService : ApplicationService
    {
        protected Zero1FiveAppService()
        {
            LocalizationResource = typeof(Zero1FiveResource);
        }
    }
}
