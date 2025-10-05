namespace Application.Interfaces.Services;

public interface IMailService
{
    Task SendDefaultMail(string to, string subject, string content);
    Task SendConfirmationMail(string to, string token);
    Task SendInvitation(string to, string campaignName, Guid campaignId, string token);
}