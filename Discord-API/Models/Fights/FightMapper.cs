using Boxed.Mapping;
using MessagingContracts.RPG;

namespace Models.Fights;

public class FightMapper : IMapper<Fight, FightConcluded>
{
    public void Map(Fight source, FightConcluded destination)
    {
        destination.FightId = source.Id;
        destination.Player = source.Player.Id;
        destination.Summary = source.Summary;
    }

    public void Map(Fight source, FightRequested destination)
    {
        destination.Player = source.Player.Id;
    }
}