namespace Application.Core.ApiReponse;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string[]? Errors { get; set; }

    public static ApiResponse Success => new ApiResponse()
    {
        IsSuccess = true,
        Errors = null
    };

    public static ApiResponse Failure(string[] errors) => new ApiResponse()
    {
        IsSuccess = false,
        Errors = errors
    };

}