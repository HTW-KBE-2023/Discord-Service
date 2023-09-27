using API.Models;
using Models.Players;

namespace Models.Fights
{
    public class Fight : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Player Player { get; set; } = new Player();
        public IList<string> Summary { get; set; } = new List<string>();
        public bool Completed { get; set; }
    }
}