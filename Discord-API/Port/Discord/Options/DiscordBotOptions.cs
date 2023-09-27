using Discord;

namespace API.Services.Discord.Options
{
    public class DiscordBotOptions
    {
        public TokenType TokenType { get; init; } = TokenType.Bot;
        public string Token { get; init; } = string.Empty;
        public IConfiguration? Configuration { get; internal set; }
    }
}