using Application.Interfaces.Contracts.Users;
using Application.Interfaces.Services;
using MassTransit;

namespace Application.Consumers.Users;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IAuthService _authService;
    private readonly IMailService _mailService;

    public UserCreatedConsumer(IAuthService authService, IMailService mailService)
    {
        _authService = authService;
        _mailService = mailService;
    }
    
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;
        var tokenResponse = await _authService.GetValidToken(message.UserMail);
        if (tokenResponse is { IsSuccess: true, Data.Token: not null })  
            await _mailService.SendConfirmationMail(message.UserMail, tokenResponse.Data.Token);
    }
}