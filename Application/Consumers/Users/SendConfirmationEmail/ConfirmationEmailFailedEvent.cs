namespace Contracts.UserRegistration;

public record ConfirmationEmailFailedEvent
{
    public Guid   CorrelationId { get; init; }
    public string Reason        { get; init; } = default!;
}
