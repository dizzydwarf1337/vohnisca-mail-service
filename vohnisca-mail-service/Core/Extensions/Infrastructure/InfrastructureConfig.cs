using Application.Commands.SendMail;
using Application.Consumers.Compaigns;
using Application.Consumers.Users.SendConfirmationEmail;
using Contracts.UserRegistration;
using MassTransit;

namespace vohnisca_mail_service.Core.Extensions.Infrastructure;

public static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<SendConfirmationEmailConsumer>();
            x.AddConsumer<InvitationCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseRawJsonDeserializer();

                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // Exchange name mappings shared with the saga orchestrator
                cfg.Message<SendConfirmationEmailCommand> (m => m.SetEntityName("send-confirmation-email"));
                cfg.Message<ConfirmationEmailSentEvent>   (m => m.SetEntityName("confirmation-email-sent"));
                cfg.Message<ConfirmationEmailFailedEvent> (m => m.SetEntityName("confirmation-email-failed"));

                // Receive commands from the saga, publish replies back
                cfg.ReceiveEndpoint("mail-service-send-confirmation", e =>
                {
                    e.Bind("send-confirmation-email");
                    e.ConfigureConsumer<SendConfirmationEmailConsumer>(context);
                });

                // Campaign invitation flow — unchanged
                cfg.ReceiveEndpoint("invitation-created", e =>
                {
                    e.ConfigureConsumer<InvitationCreatedConsumer>(context);
                });
            });
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(SendMailCommand).Assembly);
        });

        services.AddLogging();
        return services;
    }
}
