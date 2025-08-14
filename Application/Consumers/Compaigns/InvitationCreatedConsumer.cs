using Application.Interfaces.Contracts.Campaigns;
using Application.Interfaces.Services;
using MassTransit;

namespace Application.Consumers.Compaigns;

public class InvitationCreatedConsumer : IConsumer<InvitationCreatedEvent>
{
    private readonly IAuthService _authService;
    private readonly IMailService _mailService;

    public InvitationCreatedConsumer(IAuthService authService, IMailService mailService)
    {
        _authService = authService;
        _mailService = mailService;
    }
    
    public async Task Consume(ConsumeContext<InvitationCreatedEvent> context)
    {
        var message = context.Message;
        var tokenResponse = _authService.GetValidToken(message.To);
        await _mailService.SendInvitation(message.To, message.CampaignName, message.CampaignId, "SecretToken");
    }
}