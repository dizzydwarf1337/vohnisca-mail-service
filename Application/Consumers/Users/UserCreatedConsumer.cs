using Application.Interfaces.Contracts.Users;
using Application.Interfaces.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Users;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IMailService _mailService;
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(IMailService mailService, ILogger<UserCreatedConsumer> logger)
    {
        _mailService = mailService;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        try
        {
            var message = context.Message;
            await _mailService.SendConfirmationMail(message.UserMail, message.Token);
            _logger.LogInformation("Sending mail - UserCreatedEvent");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending mail");
        }
    }
}