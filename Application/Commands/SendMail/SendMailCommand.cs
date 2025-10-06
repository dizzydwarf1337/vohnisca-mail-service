using MediatR;

namespace Application.Commands.SendMail;

public class SendMailCommand : IRequest<SendMailCommand.Result>
{
    public required string Email { get; set; }
    public required string Subject { get; set; }
    public required string Content { get; set; }

    public record Result(bool IsSuccess, string? Error = null);
}