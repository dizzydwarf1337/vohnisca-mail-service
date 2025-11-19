using Application.Interfaces.Contracts.Campaigns;
using Application.Interfaces.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Compaigns;

public class InvitationCreatedConsumer : IConsumer<InvitationCreatedEvent>
{
    private readonly IMailService _mailService;
    private readonly ILogger<InvitationCreatedConsumer> _logger;

    public InvitationCreatedConsumer(IMailService mailService, ILogger<InvitationCreatedConsumer> logger)
    {
        _mailService = mailService;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<InvitationCreatedEvent> context)
    {
        try
        {
            var message = context.Message;
            await _mailService.SendInvitation(message.To, message.CampaignName, message.CampaignId);
            _logger.LogInformation("Sending email - InvitationCreatedEvent");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email");
        }

    }
}