using Boxed.Mapping;
using MessagingContracts.RPG;

namespace Models.Fights;

public class FightMapper : IMapper<Fight, FightConcluded>
{
    public void Map(Fight source, FightConcluded destination)
    {
        destination.Id = source.Id;
        destination.Player = source.Player.Id;
        destination.Summary = source.Summary;
    }

    public void Map(Fight source, FightRequested destination)
    {
        destination.Id = source.Id;
        destination.Player = source.Player.Id;
    }
}