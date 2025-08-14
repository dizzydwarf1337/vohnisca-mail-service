namespace Application.Interfaces.Services;

public interface IMailService
{
    Task SendMail(string to, string subject, string body);
    Task SendConfirmationMail(string to, string token);
    Task SendInvitation(string to, string campaignName, Guid campaignId, string token);
}