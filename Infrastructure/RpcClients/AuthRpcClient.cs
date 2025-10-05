using Application.Interfaces.Services;
using EdjCase.JsonRpc.Client;

namespace Infrastructure.RpcClients;

public class AuthRpcClient : IAuthService
{
    private readonly RpcClient _rpcClient;

    public AuthRpcClient(RpcClient rpcClient)
    {
        _rpcClient = rpcClient;
    }
    
    public async Task<TokenResponse> GetValidToken(string userMail)
    {
        var parameters = new RpcParameters(new Dictionary<string, object>
        {
            { "user_mail", userMail }
        });
        var response = await _rpcClient.SendAsync<TokenResponse>(
            new RpcRequest(
                "get_mail_confirmation_token",
                parameters
            ));
        return new TokenResponse(response.Result.Token);
    }
}