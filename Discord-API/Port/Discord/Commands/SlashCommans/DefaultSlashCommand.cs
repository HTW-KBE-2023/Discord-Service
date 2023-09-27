using Discord.WebSocket;

namespace API.Port.Discord.Commands.SlashCommans
{
    public class DefaultSlashCommand : IDiscordCommand<SocketSlashCommand>
    {
        public string Name => "Default";

        public Func<SocketSlashCommand, Task> Execute => CommandExecution;

        private async Task CommandExecution(SocketSlashCommand command)
        {
            await command.RespondAsync("Sorry I don't know what to do!");
        }
    }
}