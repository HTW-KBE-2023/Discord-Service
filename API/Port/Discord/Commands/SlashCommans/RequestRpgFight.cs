using API.Models.DiscordUsers;
using API.Services;
using Discord.WebSocket;
using MassTransit;
using MessagingContracts.RPG;

namespace API.Port.Discord.Commands.SlashCommans
{
    public class RequestRpgFight : IDiscordCommand<SocketSlashCommand>
    {
        private readonly IBus _bus;
        private readonly IGenericService<DiscordUser> _userService;

        public RequestRpgFight(IBus bus, IGenericService<DiscordUser> userService)
        {
            _bus = bus;
            _userService = userService;
        }

        public string Name => nameof(RequestRpgFight);

        public Func<SocketSlashCommand, Task> Execute => CommandExecution;

        private async Task CommandExecution(SocketSlashCommand command)
        {
            await command.RespondAsync("Youre Fight is Requested, good Luck!", ephemeral: true);

            var userDiscordId = command.User.Id;
            var user = _userService.GetAll().FirstOrDefault(user => user.DiscordId == userDiscordId);

            if (user is null)
            {
                user = new() { Id = Guid.NewGuid(), DiscordId = userDiscordId };
                _userService.Create(user);
            }

            await _bus.Send(new FightRequested { Player = user.Id });
        }
    }
}