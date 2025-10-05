using Application.Core.ApiReponse;
using MediatR;

namespace Application.Commands.SendMail;

public class SendMailCommand : IRequest<ApiResponse>
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}