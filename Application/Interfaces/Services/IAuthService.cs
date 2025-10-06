namespace Application.Interfaces.Services;

public interface IAuthService
{
    Task<RpcResult<TokenResponse>> GetValidToken(string userMail);
}

public record TokenResponse(string? Token);

