using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Users;
using Zero1Five.Emailing;

namespace Zero1Five.Products.Events
{
    public abstract class
        NotificationEventHandlerBase<TEntity, TEventHandler> : ILocalEventHandler<IEventData<TEntity>>,
            ITransientDependency
    {
        protected virtual IExternalUserLookupServiceProvider UserLookupServiceProvider { get; set; }
        protected virtual IBackgroundJobManager BackgroundJobManager { get; set; }
        protected virtual IEmailService EmailService { get; set; }
        protected virtual ILogger<TEventHandler> Logger { get; set; }

        protected abstract IEventData<TEntity> EventData { get; set; }
        protected abstract IUserData ToUser { get; set; }

        protected NotificationEventHandlerBase(
            IExternalUserLookupServiceProvider userLookupServiceProvider,
            IBackgroundJobManager backgroundJobManager,
            IEmailService emailService,
            ILogger<TEventHandler> logger)
        {
            UserLookupServiceProvider = userLookupServiceProvider;
            BackgroundJobManager = backgroundJobManager;
            EmailService = emailService;
            Logger = logger;
        }

        public async Task HandleEventAsync(IEventData<TEntity> eventData)
        {
            try
            {
                await InitialProperties(eventData);
                await TrySendNotificationAsync();
            }
            catch (System.Exception ex)
            {
                Logger.LogException(ex, LogLevel.Error);
                throw;
            }
        }

        protected abstract Task InitialProperties(IEventData<TEntity> eventData);

        protected virtual async Task TrySendNotificationAsync()
        {
            if (ToUser.EmailConfirmed)
            {
                Logger.LogInformation($" Send Email Job : Sending email to User {GetToUserFullNames()} ");
                await SendAsync();
            }
            else
                Logger.LogWarning($"Failed to send Email :To User {GetToUserFullNames()},User has not Confirmed Email");
        }

        protected virtual string GetToUserFullNames() => ToUser.Name + " " + ToUser.Surname;

        protected virtual async Task SendAsync()
        {
            await SendEmailAsync();
        }

        protected abstract Task SendEmailAsync();
    }
}