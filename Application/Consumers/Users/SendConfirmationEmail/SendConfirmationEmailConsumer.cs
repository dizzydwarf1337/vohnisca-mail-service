using Application.Interfaces.Services;
using Contracts.UserRegistration;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Users.SendConfirmationEmail;

public class SendConfirmationEmailConsumer : IConsumer<SendConfirmationEmailCommand>
{
    private readonly IMailService _mailService;
    private readonly ILogger<SendConfirmationEmailConsumer> _logger;

    public SendConfirmationEmailConsumer(
        IMailService mailService,
        ILogger<SendConfirmationEmailConsumer> logger)
    {
        _mailService = mailService;
        _logger      = logger;
    }

    public async Task Consume(ConsumeContext<SendConfirmationEmailCommand> context)
    {
        try
        {
            await _mailService.SendConfirmationMail(
                context.Message.Email,
                context.Message.Token);

            await context.Publish(new ConfirmationEmailSentEvent
            {
                CorrelationId = context.Message.CorrelationId
            });

            _logger.LogInformation(
                "[SendConfirmationEmail] Email sent to {Email}",
                context.Message.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "[SendConfirmationEmail] Failed to send email to {Email}",
                context.Message.Email);

            await context.Publish(new ConfirmationEmailFailedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                Reason        = ex.Message
            });
        }
    }
}
