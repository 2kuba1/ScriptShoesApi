using DiscordMessenger;

namespace ScriptShoesAPI.Services.DiscordLogger;


public class DiscordLoggerService : IDiscordLoggerService
{
    private readonly IConfiguration _configuration;

    public DiscordLoggerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task LogException(string tittle, string description, DiscordLoggerColors color)
    {
        new DiscordMessage()
            .SetUsername("Exception Logg")
            .SetAvatar("https://cdn.discordapp.com/attachments/1022135580399783959/1022138103516889098/logger.png")
            .AddEmbed()
            .SetTimestamp(DateTime.Now)
            .SetTitle(tittle)
            .SetDescription(description)
            .SetColor((int)color)
            .SetFooter("Log from",
                "https://cdn.discordapp.com/attachments/1022135580399783959/1022138103516889098/logger.png")
            .Build()
            .SendMessage(
                _configuration.GetValue<string>("DiscordWebHooks:ExceptionWebHook"));

        return Task.CompletedTask;
    }

    public Task LogServerError(string tittle, string description, DiscordLoggerColors color)
    {
        new DiscordMessage()
            .SetUsername("Server Error")
            .SetAvatar("https://cdn.discordapp.com/attachments/1022135580399783959/1022138103516889098/logger.png")
            .AddEmbed()
            .SetTimestamp(DateTime.Now)
            .SetTitle(tittle)
            .SetDescription(description)
            .SetColor((int)color)
            .SetFooter("Log from",
                "https://cdn.discordapp.com/attachments/1022135580399783959/1022138103516889098/logger.png")
            .Build()
            .SendMessage(
                _configuration.GetValue<string>("DiscordWebHooks:ServerErrorWebHook"));

        return Task.CompletedTask;
    }

    public Task LogInformation(string tittle, string description, DiscordLoggerColors color)
    {
        new DiscordMessage()
            .SetUsername("Information")
            .SetAvatar("https://cdn.discordapp.com/attachments/1022135580399783959/1022138103516889098/logger.png")
            .AddEmbed()
            .SetTimestamp(DateTime.Now)
            .SetTitle(tittle)
            .SetDescription(description)
            .SetColor((int)color)
            .SetFooter("Log from",
                "https://cdn.discordapp.com/attachments/1022135580399783959/1022138103516889098/logger.png")
            .Build()
            .SendMessage(
                _configuration.GetValue<string>("DiscordWebHooks:InformationLogs"));

        return Task.CompletedTask;
    }
}