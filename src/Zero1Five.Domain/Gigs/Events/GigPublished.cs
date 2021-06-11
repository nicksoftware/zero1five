using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Users;
using Zero1Five.Emailing;

namespace Zero1Five.Gigs
{
    public class GigPublicationChangeEvent
    {
        public GigPublicationChangeEvent(Gig gig)
        {
            Gig = gig;
        }
        public Gig Gig { get; set; }
    }

    public class GigPublicationChangeEventHandler:ILocalEventHandler<GigPublicationChangeEvent>,ITransientDependency
    {
        private readonly IEmailService _emailService;
        private readonly IExternalUserLookupServiceProvider _userLookupServiceProvider;
        private readonly ILogger<GigPublicationChangeEventHandler> _logger;

        public GigPublicationChangeEventHandler(
            IEmailService emailService,
            IExternalUserLookupServiceProvider userLookupServiceProvider,
            ILogger<GigPublicationChangeEventHandler> logger)
        {
            _emailService = emailService;
            _userLookupServiceProvider = userLookupServiceProvider;
            _logger = logger;
        }
        public async Task HandleEventAsync(GigPublicationChangeEvent eventData)
        {
            try
            {
                await SendGigNotificationAsync(eventData.Gig);
            }
            catch(Exception exception)
            {
                _logger.LogError("Failed to Notify user Reason :" + exception.Message);
            }
        }
        
        private async Task SendGigNotificationAsync(Gig gig)
        {
            if (gig.CreatorId == null) return;
            
            var toUser = await _userLookupServiceProvider.FindByIdAsync((Guid) gig.CreatorId);
            
            Debug.Assert(gig.LastModificationTime != null, "gig.LastModificationTime != null");
            
            var modificationDate =(DateTime)gig.LastModificationTime;
            var publicationState = gig.IsPublished ? "Published" : "Unpublished";
            
            var body = $"Your gig  \"{gig.Title}\" was successfully {publicationState} at {modificationDate:h:mm:ss tt, dd MMM yyyy}";
            
            await _emailService.SendMessageAsync(toUser.Email, "Gig Created", body);
        }
    }
}