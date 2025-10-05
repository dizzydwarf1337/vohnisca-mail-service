using Application.Interfaces.Services;
using EdjCase.JsonRpc.Client;
using Infrastructure.RpcClients;

namespace vohnisca_mail_service.Core.ServiceConfiguration;

public static class HttpRpcClients
{
    public static IServiceCollection AddHttpRpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var authServiceUri = new Uri(configuration["RpcServices:AuthService"] ?? "");
            
        services.AddSingleton<IAuthService>(_ =>
        {
            var httpClient = new HttpRpcClientBuilder(authServiceUri)
                .ConfigureHttp(opt =>
                {
                    opt.Headers = [("Accept", "application/json")];
                })
                .Build();
            return new AuthRpcClient(httpClient);
        });

        return services;
    }
}