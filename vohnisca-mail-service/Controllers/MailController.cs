using Application.Commands.SendMail;
using EdjCase.JsonRpc.Router;
using EdjCase.JsonRpc.Router.Abstractions;
using MediatR;

namespace vohnisca_mail_service.Controllers;

[RpcRoute("")]
public class MailController : RpcController
{
    private readonly IMediator  _mediator;
    
    public MailController(IMediator mediator)
        => _mediator = mediator;

    public async Task<IRpcMethodResult> SendMail(string email, string subject, string content)
    {
        var command = new SendMailCommand()
        {
            Email = email,
            Subject = subject,
            Content = content
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}