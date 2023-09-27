using API.Services.Dice;
using Moq;
using Xunit;

namespace API.Test.Services.Dices
{
    public class DiceServiceTest
    {
        [Fact]
        public void WhenD20DiceThenMaxValueShouldBe20()
        {
            IDiceService diceService = new DiceService(Random.Shared, DiceValue.D20);

            Assert.Equal(20, diceService.MaxValue);
        }

        [Fact]
        public void WhenDiceServiceCreatedThenMinValueShouldBeOne()
        {
            IDiceService diceService = new DiceService(Random.Shared, DiceValue.D20);

            Assert.Equal(1, diceService.MinValue);
        }

        [Fact]
        public void WhenD20DiceThenRolledValueShouldBeInRange()
        {
            var fakeRandom = new Mock<Random>();
            fakeRandom.Setup(random => random.Next(1, 20)).Returns(10);

            IDiceService diceService = new DiceService(fakeRandom.Object, DiceValue.D20);

            var rolledValue = diceService.Roll();

            Assert.InRange(rolledValue, 1, 20);
        }

        [Fact]
        public void WhenRolledNineThenRollAgainstTenShouldBeFailed()
        {
            var fakeRandom = new Mock<Random>();
            fakeRandom.Setup(random => random.Next(1, 20)).Returns(9);

            IDiceService diceService = new DiceService(fakeRandom.Object, DiceValue.D20);

            var rolledResult = diceService.RollAgainst(10);

            Assert.False(rolledResult);
        }

        [Fact]
        public void WhenRolledTenThenRollAgainstTenShouldBeSucessfull()
        {
            var fakeRandom = new Mock<Random>();
            fakeRandom.Setup(random => random.Next(1, 20)).Returns(10);

            IDiceService diceService = new DiceService(fakeRandom.Object, DiceValue.D20);

            var rolledResult = diceService.RollAgainst(10);

            Assert.True(rolledResult);
        }

        [Fact]
        public void WhenRolledElevenThenRollAgainstTenShouldBeSucessfull()
        {
            var fakeRandom = new Mock<Random>();
            fakeRandom.Setup(random => random.Next(1, 20)).Returns(11);

            IDiceService diceService = new DiceService(fakeRandom.Object, DiceValue.D20);

            var rolledResult = diceService.RollAgainst(10);

            Assert.True(rolledResult);
        }
    }
}