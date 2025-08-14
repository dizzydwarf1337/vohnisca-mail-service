using Application.Consumers.Compaigns;
using Application.Consumers.Users;
using MassTransit;

namespace vohnisca_mail_service.Core.Extensions.Infrastructure;

public static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.AddConsumer<InvitationCreatedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("user-created", e =>
                {
                    e.ConfigureConsumer<UserCreatedConsumer>(context);
                });
                cfg.ReceiveEndpoint("invitation-created", e =>
                {
                    e.ConfigureConsumer<InvitationCreatedConsumer>(context);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        return services;
    }
}