using API.Models.Monsters;
using API.Models.Monsters.Requests;
using API.Models.Monsters.Responses;
using API.Services.Monsters;
using Boxed.Mapping;

namespace API.Test.Services.Monsters
{
    public class MonsterMapperTest

    {
        [Fact]
        public void WhenMonsterIsMappedToMonsterResponseThenPropertiesShouldBeTheSame()
        {
            IMapper<Monster, MonsterResponse> mapper = new MonsterMapper();
            var monster = new Monster()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Level = 1,
                BaseHealth = 1,
                BaseDefence = 1,
                BaseAttack = 1,
                LevelMultiplierHealth = 2,
                LevelMultiplierDefence = 2,
                LevelMultiplierAttack = 2
            };

            var response = mapper.Map(monster);

            Assert.Equal(monster.Id, response.Id);
            Assert.Equal(monster.Name, response.Name);
            Assert.Equal(monster.Level, response.Level);
            Assert.Equal(monster.BaseHealth, response.BaseHealth);
            Assert.Equal(monster.BaseDefence, response.BaseDefence);
            Assert.Equal(monster.BaseAttack, response.BaseAttack);
            Assert.Equal(monster.LevelMultiplierHealth, response.LevelMultiplierHealth);
            Assert.Equal(monster.LevelMultiplierDefence, response.LevelMultiplierDefence);
            Assert.Equal(monster.LevelMultiplierAttack, response.LevelMultiplierAttack);
            Assert.Equal(monster.MaxHealth, response.MaxHealth);
            Assert.Equal(monster.Health, response.Health);
            Assert.Equal(monster.Defence, response.Defence);
            Assert.Equal(monster.Attack, response.Attack);
            Assert.Equal(monster.Experience, response.Experience);
        }

        [Fact]
        public void WhenMonsterIsMappedToUpdateMonsterRequestThenPropertiesShouldBeTheSame()
        {
            IMapper<UpdateMonsterRequest, Monster> mapper = new MonsterMapper();
            var updateRequest = new UpdateMonsterRequest()
            {
                Name = "Test",
                Level = 1,
                BaseHealth = 1,
                BaseDefence = 1,
                BaseAttack = 1,
                LevelMultiplierHealth = 2,
                LevelMultiplierDefence = 2,
                LevelMultiplierAttack = 2
            };

            var monster = mapper.Map(updateRequest);

            Assert.Equal(updateRequest.Name, monster.Name);
            Assert.Equal(updateRequest.Level, monster.Level);
            Assert.Equal(updateRequest.BaseHealth, monster.BaseHealth);
            Assert.Equal(updateRequest.BaseDefence, monster.BaseDefence);
            Assert.Equal(updateRequest.BaseAttack, monster.BaseAttack);
            Assert.Equal(updateRequest.LevelMultiplierHealth, monster.LevelMultiplierHealth);
            Assert.Equal(updateRequest.LevelMultiplierDefence, monster.LevelMultiplierDefence);
            Assert.Equal(updateRequest.LevelMultiplierAttack, monster.LevelMultiplierAttack);
        }

        [Fact]
        public void WhenMonsterIsMappedToCreateMonsterRequestThenPropertiesShouldBeTheSame()
        {
            IMapper<CreateMonsterRequest, Monster> mapper = new MonsterMapper();
            var createRequest = new CreateMonsterRequest()
            {
                Name = "Test",
                Level = 1,
                BaseHealth = 1,
                BaseDefence = 1,
                BaseAttack = 1,
                LevelMultiplierHealth = 2,
                LevelMultiplierDefence = 2,
                LevelMultiplierAttack = 2
            };

            var monster = mapper.Map(createRequest);

            Assert.Equal(createRequest.Id, monster.Id);
            Assert.Equal(createRequest.Name, monster.Name);
            Assert.Equal(createRequest.Level, monster.Level);
            Assert.Equal(createRequest.BaseHealth, monster.BaseHealth);
            Assert.Equal(createRequest.BaseDefence, monster.BaseDefence);
            Assert.Equal(createRequest.BaseAttack, monster.BaseAttack);
            Assert.Equal(createRequest.LevelMultiplierHealth, monster.LevelMultiplierHealth);
            Assert.Equal(createRequest.LevelMultiplierDefence, monster.LevelMultiplierDefence);
            Assert.Equal(createRequest.LevelMultiplierAttack, monster.LevelMultiplierAttack);
        }
    }
}