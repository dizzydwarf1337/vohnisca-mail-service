namespace Application.Interfaces.Contracts.Users;

public record UserCreatedEvent(string UserMail, string Token);