using Discord.WebSocket;

namespace API.Port.Discord.Commands
{
    public interface IDiscordCommandRegister
    {
        IList<IDiscordCommand<SocketSlashCommand>> SlashCommands { get; set; }
        IDiscordCommand<SocketSlashCommand> DefaultSlashCommand { get; }
    }
}