using Application.Interfaces.Services;
using EdjCase.JsonRpc.Client;

namespace Infrastructure.RpcClients;

public class AuthRpcClient : IAuthService
{
    private readonly RpcClient _rpcClient;

    public AuthRpcClient(RpcClient rpcClient)
        => _rpcClient = rpcClient;
    
    
    public async Task<RpcResult<TokenResponse>> GetValidToken(string userMail)
    {
        var parameters = new RpcParameters(new Dictionary<string, object>
        {
            { "userMail", userMail }
        });
        
        var request = new RpcRequest(
                  "GetMailConfirmationToken",
                  parameters 
                  );
        
        var response = await _rpcClient.SendAsync<TokenResponse>(request);
        return new RpcResult<TokenResponse>(response.HasError, response.Result, response.Error?.Message);
    }
}