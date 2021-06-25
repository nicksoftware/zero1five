using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;
using Zero1Five.Emailing;
using Zero1Five.Products.Events;

namespace Zero1Five.Gigs
{
    
    public class GigPublicationChangeEvent:IEventData<Gig>
    {
        public GigPublicationChangeEvent(Gig gig) => Entity = gig;
        public Gig Entity { get; set; }
    }

    public class GigPublicationChangeEventHandler:NotificationEventHandlerBase<Gig,GigPublicationChangeEventHandler>,ITransientDependency
    {
        public GigPublicationChangeEventHandler(
            IExternalUserLookupServiceProvider userLookupServiceProvider,
            IBackgroundJobManager backgroundJobManager, 
            IEmailService emailService,
            ILogger<GigPublicationChangeEventHandler> logger) : base(userLookupServiceProvider, backgroundJobManager, emailService,logger)
        {
        }
        protected override IEventData<Gig> EventData { get; set; }
        protected override IUserData ToUser { get; set; }
        protected override async Task InitialProperties(IEventData<Gig> eventData)
        {
            EventData = eventData;
            
            if (eventData.Entity.CreatorId != null)
            {
                ToUser = await UserLookupServiceProvider.FindByIdAsync((Guid) eventData.Entity.CreatorId);
            }
        }

        protected override async Task SendEmailAsync()
        {
            if (EventData.Entity.CreatorId == null) return;
            
            var toUser = await UserLookupServiceProvider.FindByIdAsync((Guid) EventData.Entity.CreatorId);
            
            Debug.Assert(EventData.Entity.LastModificationTime != null, "EventData.Entity.LastModificationTime != null");
            
            var modificationDate =(DateTime)EventData.Entity.LastModificationTime;
            var publicationState = EventData.Entity.IsPublished ? "Published" : "Unpublished";
            
            var body = $"Your Event \"{EventData.Entity.Title}\" was successfully {publicationState} at {modificationDate:h:mm:ss tt, dd MMM yyyy}";
            await EmailService.SendMessageAsync(toUser.Email, "Gig Created", body);
        }
    }
}