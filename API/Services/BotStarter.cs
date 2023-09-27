using API.Port.Discord;

namespace API.Services
{
    public class BotStarter : BackgroundService
    {
        private readonly IDiscordBot _bot;

        public BotStarter(IDiscordBot bot)
        {
            _bot = bot;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bot.LoginAsync();
            await _bot.StartAsync();
        }
    }
}