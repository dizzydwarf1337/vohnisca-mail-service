using Application.Core.ApiReponse;
using Application.Interfaces.Services;
using MediatR;
namespace Application.Commands.SendMail;

public class SendMailCommandHandler : IRequestHandler<SendMailCommand, ApiResponse>
{
    private readonly IMailService _mailService;
    
    public SendMailCommandHandler(IMailService mailService) =>  _mailService = mailService;
    
    public async Task<ApiResponse> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _mailService.SendDefaultMail(request.Email, request.Subject, request.Content);
            return ApiResponse.Success;
        }
        catch (Exception ex)
        {
            return ApiResponse.Failure([ex.InnerException.Message, ex.StackTrace ?? "", ex.Message]);
        }
    }
}