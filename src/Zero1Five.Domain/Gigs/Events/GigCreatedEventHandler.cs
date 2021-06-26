using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Users;
using Zero1Five.Emailing;
namespace Zero1Five.Gigs
{
    public class GigCreatedEventHandler : ILocalEventHandler<EntityCreatedEventData<Gig>>, ITransientDependency
    {
        private readonly IEmailService _emailService;
        private readonly IExternalUserLookupServiceProvider _userLookupServiceProvider;
        private readonly ILogger<GigCreatedEventHandler> _logger;

        public GigCreatedEventHandler(IEmailService emailService,
         IExternalUserLookupServiceProvider userLookupServiceProvider,
         ILogger<GigCreatedEventHandler> logger)
        {
            _emailService = emailService;
            _userLookupServiceProvider = userLookupServiceProvider;
            _logger = logger;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<Gig> eventData)
        {
            try
            { 
                await SendGigNotificationAsync(eventData.Entity);
            }
            catch(Exception exception)
            {
                _logger.LogError("Failed to Notify user Reason :" +exception.Message);
            }
        }

        private async Task SendGigNotificationAsync(Gig gig)
        {
            if (gig.CreatorId == null) return;

            var toUser = await _userLookupServiceProvider.FindByIdAsync((Guid) gig.CreatorId);
            var modificationDate =gig.CreationTime;
            
            var body = $"Your gig  \"{gig.Title}\" was successfully created at {modificationDate:h:mm:ss tt, dd MMM yyyy}";
            await _emailService.SendMessageAsync(toUser.Email, "Gig Created", body);
        }
    }
}