namespace Contracts.UserRegistration;

public record ConfirmationEmailSentEvent
{
    public Guid CorrelationId { get; init; }
}
