using System;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Users;
using Zero1Five.Emailing;

namespace Zero1Five.Products.Events
{
    public abstract class
        NotificationEventHandlerBase<TEntity, TEventHandler> : ILocalEventHandler<IEventData<TEntity>>
    {
        protected virtual IExternalUserLookupServiceProvider UserLookupServiceProvider { get; set; }
        protected virtual IBackgroundJobManager BackgroundJobManager { get; set; }
        protected virtual IEmailService EmailService { get; set; }
        public virtual ILogger<TEventHandler> Logger { get; set; }
        protected abstract IEventData<TEntity> EventData { get; set; }
        protected abstract IUserData ToUser { get; set; }

        protected NotificationEventHandlerBase(
            IExternalUserLookupServiceProvider userLookupServiceProvider,
            IBackgroundJobManager backgroundJobManager,
            IEmailService emailService, ILogger<TEventHandler> logger)
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
            if (IsUserIsNotEmpty()) return;
            if (IsEmailConfirmed()) return;
            Logger.LogInformation($" Send Email Job : Sending email to User {GetToUserFullNames()} ");
            await SendAsync();
        }

        private bool IsEmailConfirmed()
        {
            if (!ToUser.EmailConfirmed)
            {
                Logger.LogError($"Failed to send email to user : {GetToUserFullNames()} ,User has not Confirmed email");
                return true;
            }

            return false;
        }

        private bool IsUserIsNotEmpty()
        {
            if (ToUser == null)
            {
                Logger.LogWarning($"Failed to send Email : Product has no Creator Id.");
                return true;
            }

            return false;
        }

        protected virtual string GetToUserFullNames() => ToUser.Name + " " + ToUser.Surname;

        protected virtual async Task SendAsync()
        {
            await SendEmailAsync();
        }

        protected abstract Task SendEmailAsync();
    }
}