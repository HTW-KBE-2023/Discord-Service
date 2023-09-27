using API.Models;
using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Port.Database;
using API.Port.Repositories;
using API.Services;
using API.Services.Fights;
using MassTransit;
using MessagingContracts.RPG;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit.Sdk;

namespace API.Test.Services
{
    public class GenericServiceTest : IDisposable
    {
        private readonly RPGContext _context;

        public GenericServiceTest()
        {
            var options = new DbContextOptionsBuilder<RPGContext>()
                .UseInMemoryDatabase(databaseName: "RPGDatabase")
                .Options;

            _context = new RPGContext(options);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void WhenEntityIsValidThenCreateShouldAddToDatabase()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);
            var fight = new Fight()
            {
                Id = Guid.NewGuid(),
                Completed = true,
                Player = new Player(),
                Enemy = new Monster(),
                Summary = new List<string>()
                {
                    "Test 1",
                    "Test 2",
                    "Test 3"
                }
            };

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);

            var result = fightService.Create(fight);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Equal(fight, result.Value);
            Assert.Single(_context.Fights);
        }

        [Fact]
        public void WhenEntityIsNotValidThenCreateShouldReturnNegativeResult()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);

#pragma warning disable CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.
            var fight = new Fight()
            {
                Id = default,
                Player = null
            };
#pragma warning restore CS8625 // Ein NULL-Literal kann nicht in einen Non-Nullable-Verweistyp konvertiert werden.

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);

            var result = fightService.Create(fight);

            Assert.True(result.IsError);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);

            _ = result.Match(
                success =>
                {
                    Assert.Fail("Success schould not be called!");
                    return false;
                },
                failure =>
                {
                    Assert.Equal(2, failure.Errors.Count());
                    return true;
                }
            );

            Assert.Empty(_context.Fights);
        }

        [Fact]
        public void WhenEntityIsValidThenUpdateShouldUpdateDatabaseRecord()
        {
            var fightValidator = new FightValidator();
            var fight = new Fight()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Player = new Player(),
                Enemy = new Monster(),
                Summary = new List<string>()
                {
                    "Test 1",
                    "Test 2",
                    "Test 3"
                }
            };

            _context.Add(fight);
            _context.SaveChanges();

            Assert.False(_context.Fights.First().Completed);

            var unitOfWork = new UnitOfWork(_context);
            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);

            fight.Completed = true;

            var result = fightService.Update(fight);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Equal(fight, result.Value);
            Assert.Single(_context.Fights);
            Assert.True(_context.Fights.First().Completed);
        }

        [Fact]
        public void WhenEntityIsNotValidThenUpdateShouldReturnNegativeResultWithoutUpdatingDatabase()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);

            var fight = new Fight()
            {
                Id = Guid.NewGuid(),
                Summary = new List<string>() { "Ha!" },
                Completed = false
            };

            _context.Add(fight);
            _context.SaveChanges();

            Assert.False(_context.Fights.First().Completed);

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);

            fight.Id = Guid.Empty;
            fight.Completed = true;

            var result = fightService.Update(fight);

            Assert.True(result.IsError);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);

            _ = result.Match(
                success =>
                {
                    Assert.Fail("Success schould not be called!");
                    return false;
                },
                failure =>
                {
                    Assert.Single(failure.Errors);
                    return true;
                }
            );

            Assert.Single(_context.Fights);
        }

        [Fact]
        public void WhenEntityDontExistThenUpdateShouldReturnDefaultEntityWithoutUpdatingDatabase()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);

            var fight = new Fight()
            {
                Id = Guid.NewGuid(),
                Player = new Player(),
                Enemy = new Monster(),
                Completed = false
            };

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);

            fight.Completed = true;

            var result = fightService.Update(fight);

            Assert.False(result.IsError);
            Assert.True(result.IsSuccess);
            Assert.Equal(default, result.Value);

            _ = result.Match(
                success =>
                {
                    Assert.Equal(default, success);
                    return false;
                },
                failure =>
                {
                    Assert.Fail("Failed schould not be called!");
                    return true;
                }
            );

            Assert.Empty(_context.Fights);
        }

        [Fact]
        public void WhenEntitiesExistThenGetAllShouldReturnAllRecords()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);

            var fight1 = new Fight()
            {
                Id = Guid.NewGuid()
            };
            var fight2 = new Fight()
            {
                Id = Guid.NewGuid()
            };
            _context.Add(fight1);
            _context.Add(fight2);
            _context.SaveChanges();

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);
            var result = fightService.GetAll();

            Assert.NotEmpty(result);
            Assert.Contains(fight1, result);
            Assert.Contains(fight2, result);
        }

        [Fact]
        public void WhenEntityExistThenGetByIdShouldReturnThisRecord()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);

            var fight1 = new Fight()
            {
                Id = Guid.NewGuid(),
                Player = new Player(),
            };
            var fight2 = new Fight()
            {
                Id = Guid.NewGuid()
            };
            _context.Add(fight1);
            _context.Add(fight2);
            _context.SaveChanges();

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);
            var result = fightService.GetById(fight1.Id);

            Assert.NotNull(result);
            Assert.Equal(fight1, result);
            Assert.Equal(fight1.Player.Id, result.Player.Id);
        }

        [Fact]
        public void WhenEntityExistThenDeleteShouldRemoveThisRecord()
        {
            var fightValidator = new FightValidator();
            var unitOfWork = new UnitOfWork(_context);

            var fight1 = new Fight()
            {
                Id = Guid.NewGuid(),
                Player = new Player(),
            };
            var fight2 = new Fight()
            {
                Id = Guid.NewGuid()
            };
            _context.Add(fight1);
            _context.Add(fight2);
            _context.SaveChanges();

            IGenericService<Fight> fightService = new GenericService<Fight>(unitOfWork, fightValidator);
            fightService.DeleteById(fight1.Id);

            Assert.Single(_context.Fights);
            Assert.DoesNotContain(fight1, _context.Fights);
            Assert.Contains(fight2, _context.Fights);
        }
    }
}