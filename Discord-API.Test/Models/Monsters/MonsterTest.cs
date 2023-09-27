using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Services.Dice;
using Moq;
using System.Dynamic;
using System.Reflection.Emit;
using Xunit;

namespace API.Test.Models.Monsters
{
    public class MonsterTest
    {
        [Fact]
        public void WhenMonsterIsCreatedThenPropertiesShouldDefault()
        {
            var monster = new Monster();

            Assert.NotEqual(Guid.Empty, monster.Id);
            Assert.Equal("No-Name-Monster", monster.Name);
            Assert.Equal(1, monster.Level);
            Assert.Equal(1, monster.BaseHealth);
            Assert.Equal(1, monster.BaseDefence);
            Assert.Equal(1, monster.BaseAttack);
            Assert.Equal(1, monster.LevelMultiplierHealth);
            Assert.Equal(1, monster.LevelMultiplierDefence);
            Assert.Equal(1, monster.LevelMultiplierAttack);
            Assert.Equal(2, monster.MaxHealth);
            Assert.Equal(2, monster.Health);
            Assert.Equal(2, monster.Defence);
            Assert.Equal(2, monster.Attack);
            Assert.Equal(0.6, monster.Experience, 3);
        }

        [Fact]
        public void WhenMonsterTakesDamageThenHealthShouldBeReduced()
        {
            var monster = new Monster();

            monster.TakesDamage(1);

            Assert.Equal(2, monster.MaxHealth);
            Assert.Equal(1, monster.Health);
        }
    }
}