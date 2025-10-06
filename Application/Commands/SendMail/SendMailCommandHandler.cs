using Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.SendMail;

public class SendMailCommandHandler : IRequestHandler<SendMailCommand, SendMailCommand.Result>
{
    private readonly IMailService _mailService;
    private readonly ILogger<SendMailCommandHandler> _logger;

    public SendMailCommandHandler(IMailService mailService)
    {
        _mailService = mailService;
        _logger = new LoggerFactory().CreateLogger<SendMailCommandHandler>();
    }
    
    public async Task<SendMailCommand.Result> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _mailService.SendDefaultMail(request.Email, request.Subject, request.Content);
            return new SendMailCommand.Result(true);
        }
        catch(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return new SendMailCommand.Result(false, $"{ex.Message} \n {ex.StackTrace}");
        }
        
    }
}