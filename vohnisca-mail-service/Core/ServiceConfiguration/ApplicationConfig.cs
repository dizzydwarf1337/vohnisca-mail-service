using Application.Interfaces.Services;
using Infrastructure.RpcClients;
using Infrastructure.Services;

namespace vohnisca_mail_service.Core.ServiceConfiguration;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, GmailService>();
        services.AddScoped<IAuthService, AuthRpcClient>();
        return services;
    }
}