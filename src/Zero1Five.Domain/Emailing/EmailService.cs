using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.TextTemplating;

namespace Zero1Five.Emailing
{
    public interface IEmailService : ITransientDependency
    {
        Task SendWelcomeMessageAsync(string to);
        Task SendMessageAsync(string to, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;

        public EmailService(
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer)
        {
            _emailSender = emailSender;
            _templateRenderer = templateRenderer;
        }

        public async Task SendMessageAsync(string to, string subject, string message)
        {
            var body = await _templateRenderer.RenderAsync(Zero1FiveTemplates.Message,
                new
                {
                    Message = message
                });

            await _emailSender.SendAsync(to, subject, body);
        }

        public async Task SendWelcomeMessageAsync(string to)
        {
            var body = await _templateRenderer.RenderAsync(
                Zero1FiveTemplates.Welcome,
                new
                {
                    Message = "Welcome to Zero 1 Five ,Start looking and or advertising yourself."
                });

            await _emailSender.SendAsync(to, "Welcome to Zero 1 Five", body);
        }
    }
}