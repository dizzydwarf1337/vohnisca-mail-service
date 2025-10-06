namespace Application.Interfaces.Services;

public record RpcResult<T>(bool IsSuccess, T? Data, string? Error);