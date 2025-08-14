using vohnisca_mail_service.Core.Extensions.Cors;
using vohnisca_mail_service.Core.Extensions.Infrastructure;
using vohnisca_mail_service.Core.Extensions.Middleware;
using vohnisca_mail_service.Core.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure()
    .AddApplicationServices()
    .AddHttpRpcClients(builder.Configuration)
    .AddAppServices()
    .AddCorsPolicy();
var app = builder.Build();


app.UseRouting();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
