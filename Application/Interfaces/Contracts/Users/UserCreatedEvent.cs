namespace Application.Interfaces.Contracts.Users;

public record UserCreatedEvent(Guid UserId, string UserMail, string Token);