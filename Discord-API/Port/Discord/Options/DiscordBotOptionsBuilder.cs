using Discord;

namespace API.Services.Discord.Options
{
    public class DiscordBotOptionsBuilder
    {
        private TokenType _tokenType = TokenType.Bot;
        private string _token = string.Empty;

        private IConfiguration? _configuration;

        public DiscordBotOptionsBuilder WithToken(TokenType tokenType, string token)
        {
            _tokenType = tokenType;
            _token = token;

            return this;
        }

        public DiscordBotOptionsBuilder WithConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;

            return this;
        }

        public DiscordBotOptions Build()

        {
            return new()
            {
                TokenType = _tokenType,
                Token = _token,
                Configuration = _configuration
            };
        }
    }
}