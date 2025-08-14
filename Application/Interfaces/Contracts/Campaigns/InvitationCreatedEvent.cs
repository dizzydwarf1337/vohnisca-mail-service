namespace Application.Interfaces.Contracts.Campaigns;

public record InvitationCreatedEvent(string To, string CampaignName, Guid CampaignId, string Token);