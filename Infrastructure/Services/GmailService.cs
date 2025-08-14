using System.Net;
using System.Net.Mail;
using Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class GmailService : IMailService
{
    private readonly string _senderEmail;
    private readonly string _senderName;
    private readonly string _senderPassword;
    private readonly int _smtpPort;
    private readonly string _smtpServer;
    public GmailService(IConfiguration configuration)
    {
        var emailConfig = configuration.GetSection("EmailSettings");
        _smtpServer = emailConfig["SmtpServer"];
        _smtpPort = int.Parse(emailConfig["SmtpPort"]);
        _senderEmail = emailConfig["SenderEmail"];
        _senderPassword = emailConfig["SenderPassword"];
        _senderName = emailConfig["SenderName"];

    }
    public async Task SendMail(string to, string subject, string body)
    {
        using var smtp = new SmtpClient(_smtpServer, _smtpPort);
        smtp.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
        smtp.EnableSsl = true;

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_senderEmail, _senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        await smtp.SendMailAsync(mailMessage);
    }

    public async Task SendConfirmationMail(string to, string token)
    {
        var confirmationLink = $"http://localhost:3000/confirm?email={to}&token={token}";
        var body = $@"
        <body style=""margin: 0; padding: 0; background: linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f172a 100%); font-family: Arial, sans-serif;"">
    <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background: linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f172a 100%); min-height: 100vh;"">
        <tr>
            <td align=""center"" style=""padding: 20px;"">
                
                <!-- Main Container -->
                <table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""max-width: 600px; width: 100%; background: linear-gradient(135deg, #2d1b4e 0%, #1e1538 100%); border-radius: 16px; box-shadow: 0 20px 40px rgba(0,0,0,0.4); border: 1px solid rgba(255,107,53,0.2);"">
                    
                    <!-- Header -->
                    <tr>
                        <td style=""background: linear-gradient(135deg, #ff6b35 0%, #f7931e 50%, #ff4500 100%); padding: 30px 20px; text-align: center; border-radius: 15px 15px 0 0;"">
                            <h1 style=""margin: 0; color: #ffffff; font-size: 28px; font-weight: bold; text-shadow: 2px 2px 4px rgba(0,0,0,0.3);"">
                                🔥 Vohnisca
                            </h1>
                            <p style=""margin: 10px 0 0 0; color: rgba(255,255,255,0.9); font-size: 14px;"">
                                Igniting connections
                            </p>
                        </td>
                    </tr>
                    
                    <!-- Content -->
                    <tr>
                        <td style=""padding: 30px 20px;"">
                            
                            <!-- Icon and Title -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
                                <tr>
                                    <td align=""center"" style=""padding-bottom: 25px;"">
                                        <table cellpadding=""0"" cellspacing=""0"" border=""0"">
                                            <tr>
                                                <td style=""width: 70px; height: 70px; background: linear-gradient(135deg, #ff6b35, #f7931e); border-radius: 35px; text-align: center; vertical-align: middle; font-size: 28px; box-shadow: 0 8px 20px rgba(255,107,53,0.3);"">
                                                    🔥
                                                </td>
                                            </tr>
                                        </table>
                                        <h2 style=""color: #ffffff; font-size: 24px; margin: 20px 0 8px 0; font-weight: 600;"">
                                            Welcome to the Fire!
                                        </h2>
                                        <p style=""color: rgba(255,255,255,0.8); font-size: 15px; margin: 0; line-height: 1.6;"">
                                            Your account is almost ready to ignite
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Message Content -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background: rgba(255,107,53,0.05); border: 1px solid rgba(255,107,53,0.2); border-radius: 12px; margin-bottom: 25px;"">
                                <tr>
                                    <td style=""padding: 25px 20px;"">
                                        <p style=""color: rgba(255,255,255,0.9); font-size: 15px; line-height: 1.8; margin: 0 0 15px 0; text-align: center;"">
                                            Thank you for joining <strong style=""color: #ff6b35;"">Vohnisca</strong>! 
                                        </p>
                                        <p style=""color: rgba(255,255,255,0.9); font-size: 15px; line-height: 1.8; margin: 0; text-align: center;"">
                                            To complete your registration and light up your account, please confirm your email address by clicking the button below:
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- CTA Button -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
                                <tr>
                                    <td align=""center"" style=""padding: 25px 0;"">
                                        <table cellpadding=""0"" cellspacing=""0"" border=""0"">
                                            <tr>
                                                <td style=""background: linear-gradient(135deg, #ff6b35 0%, #f7931e 100%); border-radius: 50px; box-shadow: 0 8px 20px rgba(255,107,53,0.3);"">
                                                    <a href=""{{confirmationLink}}"" style=""display: block; color: #ffffff; padding: 14px 30px; text-decoration: none; font-size: 16px; font-weight: 600; text-shadow: 1px 1px 2px rgba(0,0,0,0.2);"">
                                                        🔥 Ignite My Account
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Footer Info -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""border-top: 1px solid rgba(255,255,255,0.1); padding-top: 25px;"">
                                <tr>
                                    <td align=""center"">
                                        <p style=""color: rgba(255,255,255,0.6); font-size: 13px; line-height: 1.6; margin: 0 0 15px 0; text-align: center;"">
                                            If you didn't create an account with Vohnisca, you can safely ignore this email. 
                                            No account will be created and no further emails will be sent.
                                        </p>
                                        <p style=""color: rgba(255,255,255,0.6); font-size: 13px; margin: 0; text-align: center;"">
                                            Need help? Contact us at <a href=""mailto:support@vohnisca.com"" style=""color: #ff6b35; text-decoration: none;"">support@vohnisca.com</a>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    
                    <!-- Footer -->
                    <tr>
                        <td style=""background: rgba(0,0,0,0.2); padding: 25px 20px; text-align: center; border-top: 1px solid rgba(255,255,255,0.1); border-radius: 0 0 15px 15px;"">
                            <p style=""margin: 0 0 8px 0; color: #ff6b35; font-weight: 600; font-size: 15px;"">
                                — The Vohnisca Team 🔥
                            </p>
                            <p style=""margin: 0; color: rgba(255,255,255,0.5); font-size: 11px;"">
                                This email was sent from Vohnisca. Keep the fire burning!
                            </p>
                        </td>
                    </tr>
                    
                </table>
                
            </td>
        </tr>
    </table>
</body>";

        await SendMail(to, "Email-confirmation", body);
    }

    public async Task SendInvitation(string to, string campaignName, Guid campaignId, string token)
    {
        var invitationLink =$"http://localhost:3000/invite?email={to}&campaignId={campaignId}&token={token}";;
        var body = $@"
        <body style=""margin: 0; padding: 0; background: linear-gradient(135deg, #0a0f1c 0%, #1a1a2e 50%, #2d1b4e 100%); font-family: Arial, sans-serif; min-height: 100vh;"">
    <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background: linear-gradient(135deg, #0a0f1c 0%, #1a1a2e 50%, #2d1b4e 100%); min-height: 100vh;"">
        <tr>
            <td align=""center"" style=""padding: 20px;"">
                
                <!-- Main Container -->
                <table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""max-width: 600px; width: 100%; background: linear-gradient(135deg, #1e1538 0%, #2d1b4e 100%); border-radius: 20px; box-shadow: 0 25px 50px rgba(0,0,0,0.5); border: 2px solid rgba(255,107,53,0.2); overflow: hidden;"">
                    
                    <!-- Header -->
                    <tr>
                        <td style=""background: linear-gradient(135deg, #ff6b35 0%, #d4af37 50%, #ff4500 100%); padding: 40px 20px; text-align: center; position: relative;"">
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
                                <tr>
                                    <td width=""30"" style=""font-size: 20px; color: rgba(255,255,255,0.6);"">⚔️</td>
                                    <td align=""center"">
                                        <h1 style=""margin: 0; color: #ffffff; font-size: 32px; font-weight: bold; text-shadow: 3px 3px 6px rgba(0,0,0,0.4);"">
                                            🔥 Vohnisca
                                        </h1>
                                        <p style=""margin: 10px 0 0 0; color: rgba(255,255,255,0.9); font-size: 16px; font-style: italic; text-shadow: 1px 1px 2px rgba(0,0,0,0.3);"">
                                            Where Adventures Ignite
                                        </p>
                                    </td>
                                    <td width=""30"" style=""font-size: 20px; color: rgba(255,255,255,0.6);"">🛡️</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <!-- Content -->
                    <tr>
                        <td style=""padding: 30px 20px;"">
                            
                            <!-- Scroll Container -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background: linear-gradient(135deg, rgba(212,175,55,0.1) 0%, rgba(255,107,53,0.05) 100%); border: 2px solid rgba(212,175,55,0.3); border-radius: 15px; margin-bottom: 30px;"">
                                <tr>
                                    <td style=""padding: 35px 20px;"">
                                        
                                        <!-- Icon and Title -->
                                        <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
                                            <tr>
                                                <td align=""center"" style=""padding-bottom: 25px;"">
                                                    <table cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                        <tr>
                                                            <td style=""width: 80px; height: 80px; background: linear-gradient(135deg, #d4af37 0%, #ff6b35 100%); border-radius: 40px; text-align: center; vertical-align: middle; font-size: 32px; box-shadow: 0 10px 25px rgba(212,175,55,0.4);"">
                                                                📜
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <h2 style=""color: #d4af37; font-size: 28px; margin: 20px 0 10px 0; font-weight: bold; text-shadow: 2px 2px 4px rgba(0,0,0,0.3);"">
                                                        Quest Invitation
                                                    </h2>
                                                    <p style=""color: rgba(255,255,255,0.8); font-size: 16px; margin: 0; line-height: 1.6; font-style: italic;"">
                                                        A grand adventure awaits!
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <!-- Message Content -->
                                        <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
                                            <tr>
                                                <td align=""center"">
                                                    <p style=""color: rgba(255,255,255,0.9); font-size: 16px; line-height: 1.8; margin: 20px 0; text-align: center;"">
                                                        Greetings, brave adventurer! ⚔️
                                                    </p>
                                                    <p style=""color: rgba(255,255,255,0.9); font-size: 16px; line-height: 1.8; margin: 20px 0; text-align: center;"">
                                                        You have been chosen to join an epic campaign:
                                                    </p>
                                                    
                                                    <!-- Campaign Name -->
                                                    <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""margin: 25px 0;"">
                                                        <tr>
                                                            <td style=""background: linear-gradient(135deg, rgba(255,107,53,0.2) 0%, rgba(212,175,55,0.2) 100%); border: 1px solid rgba(212,175,55,0.4); border-radius: 10px; padding: 20px; text-align: center;"">
                                                                <h3 style=""color: #d4af37; font-size: 22px; margin: 0; font-weight: bold; text-shadow: 1px 1px 2px rgba(0,0,0,0.3);"">
                                                                    🏰 {campaignName} 🏰
                                                                </h3>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    
                                                    <p style=""color: rgba(255,255,255,0.9); font-size: 16px; line-height: 1.8; margin: 20px 0; text-align: center;"">
                                                        Your party needs you! Accept this invitation to join your fellow adventurers and begin your legendary journey.
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- CTA Button -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"">
                                <tr>
                                    <td align=""center"" style=""padding: 30px 0;"">
                                        <table cellpadding=""0"" cellspacing=""0"" border=""0"">
                                            <tr>
                                                <td style=""background: linear-gradient(135deg, #d4af37 0%, #ff6b35 50%, #d4af37 100%); border-radius: 50px; box-shadow: 0 12px 30px rgba(212,175,55,0.4); border: 2px solid rgba(255,255,255,0.2);"">
                                                    <a href=""{invitationLink}"" style=""display: block; color: #1a1a2e; padding: 18px 40px; text-decoration: none; font-size: 18px; font-weight: bold; text-shadow: 1px 1px 2px rgba(255,255,255,0.3);"">
                                                        ⚔️ Join the Adventure ⚔️
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Additional Info -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""margin: 30px 0;"">
                                <tr>
                                    <td style=""background: rgba(0,0,0,0.2); border-left: 4px solid #d4af37; border-radius: 8px; padding: 20px;"">
                                        <p style=""color: #d4af37; font-size: 16px; line-height: 1.7; margin: 0 0 15px 0; font-weight: bold;"">
                                            🎲 What awaits you:
                                        </p>
                                        <p style=""color: rgba(255,255,255,0.8); font-size: 15px; line-height: 1.7; margin: 0 0 8px 0;"">
                                            • Epic storytelling and immersive roleplay
                                        </p>
                                        <p style=""color: rgba(255,255,255,0.8); font-size: 15px; line-height: 1.7; margin: 0 0 8px 0;"">
                                            • Character sheets, dice rolling, and campaign tools
                                        </p>
                                        <p style=""color: rgba(255,255,255,0.8); font-size: 15px; line-height: 1.7; margin: 0;"">
                                            • A fellowship of adventurers ready for quests
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Footer Info -->
                            <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""border-top: 1px solid rgba(212,175,55,0.3); padding-top: 25px;"">
                                <tr>
                                    <td align=""center"">
                                        <p style=""color: rgba(255,255,255,0.6); font-size: 14px; line-height: 1.6; margin: 0 0 15px 0; text-align: center;"">
                                            If you didn't expect this invitation or don't wish to join this campaign, 
                                            you can safely ignore this email. No dragons were harmed in making this message. 🐉
                                        </p>
                                        <p style=""color: rgba(255,255,255,0.6); font-size: 14px; margin: 0; text-align: center;"">
                                            Need help? Contact the Guild Masters at 
                                            <a href=""mailto:support@vohnisca.com"" style=""color: #d4af37; text-decoration: none;"">support@vohnisca.com</a>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    
                    <!-- Footer -->
                    <tr>
                        <td style=""background: linear-gradient(135deg, rgba(0,0,0,0.3) 0%, rgba(0,0,0,0.5) 100%); padding: 30px 20px; text-align: center; border-top: 1px solid rgba(212,175,55,0.3);"">
                            <p style=""margin: 0 0 8px 0; color: #d4af37; font-weight: bold; font-size: 16px; text-shadow: 1px 1px 2px rgba(0,0,0,0.3);"">
                                — The Guild Masters of Vohnisca 🔥⚔️
                            </p>
                            <p style=""margin: 0; color: rgba(255,255,255,0.5); font-size: 12px; font-style: italic;"">
                                ""In the flames of Vohnisca, legends are forged and adventures ignite""
                            </p>
                        </td>
                    </tr>
                    
                </table>
                
            </td>
        </tr>
    </table>
</body>
        ";
        await SendMail(to, "New Invitation 🔥⚔️", body);
    }
}