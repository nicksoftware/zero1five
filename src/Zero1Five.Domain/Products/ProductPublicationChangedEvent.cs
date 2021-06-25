using System;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;
using Zero1Five.Emailing;
using Zero1Five.Emailing.BackgroundJobs;
using Zero1Five.Products.Events;

namespace Zero1Five.Products
{
    public class ProductPublicationChangedEvent:IEventData<Product>
    {

        public ProductPublicationChangedEvent(Product entity)
        {
            Entity = entity;
        }
        public Product Entity { get; set; }
    }
    public  class  ProductPublicationChangedEventHandler:NotificationEventHandlerBase<Product,ProductPublicationChangedEventHandler>,ITransientDependency
    {
        public ProductPublicationChangedEventHandler(
            IExternalUserLookupServiceProvider userLookupServiceProvider,
            IBackgroundJobManager backgroundJobManager, 
            IEmailService emailService,
            ILogger<ProductPublicationChangedEventHandler> logger) : base(userLookupServiceProvider, backgroundJobManager, emailService,logger)
        {
        }

        protected override IEventData<Product> EventData { get; set; }
        protected override IUserData ToUser { get; set; }
        protected override async Task InitialProperties(IEventData<Product> eventData)
        {
            EventData = eventData;
            
            if (eventData.Entity.CreatorId != null)
            {
                ToUser = await UserLookupServiceProvider.FindByIdAsync((Guid) eventData.Entity.CreatorId);
            }
        }
        protected override async Task SendEmailAsync()
        {
            var publicationState = EventData.Entity.IsPublished ? "Published" : "Unpublished";
            var time = EventData.Entity.LastModificationTime.Humanize();
            var message = $"your product named {EventData.Entity.Title} was {publicationState} {time}";

            if (ToUser != null)
            {
                await BackgroundJobManager.EnqueueAsync(new EmailSendingArgs()
                {
                    EmailAddress = ToUser.Email,
                    Subject = $"Product {publicationState}",
                    Body = message
                });
            }
        }
    }
}