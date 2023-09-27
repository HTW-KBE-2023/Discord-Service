namespace API.Port.Discord.Commands
{
    public interface IDiscordCommand<TCommand>
    {
        public string Name { get; }
        Func<TCommand, Task> Execute { get; }
    }
}