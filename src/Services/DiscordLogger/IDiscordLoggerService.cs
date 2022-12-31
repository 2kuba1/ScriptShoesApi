namespace ScriptShoesCQRS.Services.DiscordLogger;

public interface IDiscordLoggerService
{
    Task LogException(string tittle, string description, DiscordLoggerColors color);
    Task LogServerError(string tittle, string description, DiscordLoggerColors color);
    Task LogInformation(string tittle, string description, DiscordLoggerColors color);
}