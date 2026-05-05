namespace Contracts.UserRegistration;

public record SendConfirmationEmailCommand
{
    public Guid   CorrelationId { get; init; }
    public Guid   UserId        { get; init; }
    public string Email         { get; init; } = default!;
    public string Token         { get; init; } = default!;
}
