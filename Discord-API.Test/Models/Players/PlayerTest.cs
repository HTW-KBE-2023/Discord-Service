using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Services.Dice;
using Moq;
using System.Dynamic;
using Xunit;

namespace API.Test.Models.Players
{
    public class PlayerTest
    {
        [Fact]
        public void WhenPlayerIsCreatedThenPropertiesShouldDefault()
        {
            var player = new Player();

            Assert.NotEqual(Guid.Empty, player.Id);
            Assert.Equal("No-Name-Player", player.Name);
            Assert.Equal(1, player.Level);
            Assert.Equal(5, player.Health);
            Assert.Equal(1, player.Defence);
            Assert.Equal(1, player.Attack);
            Assert.Equal(0, player.Experience);
        }

        [Fact]
        public void WhenPlayerTakesDamageThenHealthShouldBeReduced()
        {
            var player = new Player();

            player.TakesDamage(1);

            Assert.Equal(4, player.Health);
        }
    }
}