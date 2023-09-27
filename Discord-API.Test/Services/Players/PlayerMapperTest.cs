using API.Models.Players;
using API.Models.Players.Responses;
using API.Services.Players;
using Boxed.Mapping;

namespace API.Test.Services.Players
{
    public class PlayerMapperTest

    {
        [Fact]
        public void WhenPlayerIsMappedToPlayerResponseThenPropertiesShouldBeTheSame()
        {
            IMapper<Player, PlayerResponse> mapper = new PlayerMapper();
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = "Test-Player",
                Level = 1,
                Health = 8,
                Defence = 4,
                Attack = 2,
                Experience = 40
            };

            var response = mapper.Map(player);

            Assert.Equal(player.Id, response.Id);
            Assert.Equal(player.Name, response.Name);
            Assert.Equal(player.Level, response.Level);
            Assert.Equal(player.Health, response.Health);
            Assert.Equal(player.Defence, response.Defence);
            Assert.Equal(player.Attack, response.Attack);
            Assert.Equal(player.Experience, response.Experience);
        }
    }
}