using Models.Players;

namespace API.Models.DiscordUsers
{
    public class DiscordUser : IEntity
    {
        public Guid Id { get; set; }
        public ulong DiscordId { get; set; }

        public Player? Player { get; set; }
    }
}