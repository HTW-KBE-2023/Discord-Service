using API.Models.Fights;

namespace API.Test.Models.Fights
{
    public class FightTest
    {
        [Fact]
        public void WhenFightIsCreatedThenIdShouldNotBeEmpty()
        {
            var fight = new Fight();

            Assert.NotEqual(Guid.Empty, fight.Id);
        }

        [Fact]
        public void WhenFightIsCreatedThenPlayerShouldBeDefault()
        {
            var fight = new Fight();

            Assert.NotNull(fight.Player);
        }

        [Fact]
        public void WhenFightIsCreatedThenEnemyShouldBeDefault()
        {
            var fight = new Fight();

            Assert.NotNull(fight.Enemy);
        }

        [Fact]
        public void WhenFightIsCreatedThenSummaryShouldNotBeNullButEmpty()
        {
            var fight = new Fight();

            Assert.NotNull(fight.Summary);
            Assert.Empty(fight.Summary);
        }

        [Fact]
        public void WhenFightIsCreatedThenCompletedShouldNotBeFalse()
        {
            var fight = new Fight();

            Assert.False(fight.Completed);
        }
    }
}