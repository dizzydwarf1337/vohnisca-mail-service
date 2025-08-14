namespace Application.Interfaces.Services;

public interface IAuthService
{
    Task<TokenResponse> GetValidToken(string userMail);
}

public record TokenResponse(string Token);