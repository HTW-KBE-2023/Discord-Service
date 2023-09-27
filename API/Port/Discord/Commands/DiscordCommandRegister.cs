using Discord.WebSocket;

namespace API.Port.Discord.Commands
{
    public class DiscordCommandRegister : IDiscordCommandRegister
    {
        public IList<IDiscordCommand<SocketSlashCommand>> SlashCommands { get; set; }
        public IDiscordCommand<SocketSlashCommand> DefaultSlashCommand { get; set; }
    }
}