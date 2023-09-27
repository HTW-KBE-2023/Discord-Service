using API.Models.DiscordUsers;
using API.Services;
using Discord.WebSocket;
using MassTransit;
using MessagingContracts.RPG;

namespace API.Port.Discord.Commands.SlashCommans
{
    public class RequestRpgFight : IDiscordCommand<SocketSlashCommand>
    {
        private readonly ILogger<RequestRpgFight> _logger;
        private readonly IBus _bus;
        private readonly IGenericService<DiscordUser> _userService;

        public RequestRpgFight(ILogger<RequestRpgFight> logger, IBus bus, IGenericService<DiscordUser> userService)
        {
            _logger = logger;
            _bus = bus;
            _userService = userService;
        }

        public string Name => "request-rpg-fight";

        public Func<SocketSlashCommand, Task> Execute => CommandExecution;

        private async Task CommandExecution(SocketSlashCommand command)
        {
            await command.RespondAsync("Youre Fight is Requested, good Luck!", ephemeral: true);

            var userDiscordId = command.User.Id;
            var user = _userService.GetAll().FirstOrDefault(user => user.DiscordId == userDiscordId);

            _logger.LogInformation("User Exist -> {exist}", user is not null);

            if (user is null)
            {
                user = new() { Id = Guid.NewGuid(), DiscordId = userDiscordId };
                user = _userService.Create(user).Match(success => success, failed => throw new ArgumentException());

                _logger.LogInformation("User Exist -> {exist}", user is not null);
            }

            await _bus.Send(new FightRequested { Player = user.Id });
        }
    }
}