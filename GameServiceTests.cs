using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace qodeless.application.tests
{
    [TestClass]
    public class GameServicesTests
    {
        private readonly Mock<IGameRepository> gameRepositoryMock;
        private readonly IGameAppService gameAppService;
        private readonly Mapper mapper;

        public GameServicesTests()
        {
            gameRepositoryMock = new Mock<IGameRepository>();

            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<Game, GameViewModel>();
                cfg.CreateMap<GameViewModel, Game>();
            }));

            gameAppService = new GameAppService(this.mapper, this.gameRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddingGameWithCode1Digit_ShouldNotBeValid()
        {
            // given (arrange)
            var gameId = It.IsAny<Guid>();
            var game = new PlayViewModel() { Id = gameId, Name = "TESTE", Code = "1", AcceptanceCriteria = 51 };

            // when (act)
            var validationResult = await gameAppService.Add(game);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(game);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public async Task GetGameById_ShouldReturnFieldName()
        {
            // given (arrange)
            var entityId = It.IsAny<Guid>();
            var entityName = It.IsAny<string>(); /*string aleatórias*/
            var game = new Game (entityId) { Name = entityName };
            GameRepositoryMock.Setup(repository => repository.GetById(entityId)).Returns(group).Verifiable();

            // when (act)
            var gameFromService = await gameAppService.GetById(entityId);

            // then (assert)
            // 1. Actual list of groups == expected groups
            gameRepositoryMock.Verify();
            Assert.IsNotNull(game);
            Assert.AreEqual(entityName, gamesFromService.Name);
        }

        [TestMethod]
        public async Task AddingGameWithAcceptanceCriteriaLow_ShouldNotBeValid()
        {
            // given (arrange)
            var gameId = It.IsAny<Guid>();
            var game = new GameViewModel() { Id = gameId, Name = "TESTE", Code = "C123", AcceptanceCriteria = 49 };

            // when (act)
            var validationResult = await gameAppService.Add(game);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(game);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("TESTE1", "PLAY", 51)]
        [DataRow("TESTE2", "PLAY", 88)]
        public async Task AddingGameFullFilled_ShouldBeOK(string code, string name, int acceptance)
        {
            // given (arrange)
            var gameId = It.IsAny<Guid>();
            var game = new GameViewModel() { Id = gameId, Name = name, Code = code, AcceptanceCriteria = acceptance };

            // when (act)
            var validationResult = await gameAppService.Add(game);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(game);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow(null, "PLAY", 50)]
        [DataRow("", "PLAY", 50)]
        public async Task AddingGameWithCodeNull_ShouldNotBeValid(string code, string name, int acceptance)
        {
            // given (arrange)
            var gameId = It.IsAny<Guid>();
            var game = new GameViewModel() { Id = gameId, Name = name, Code = code, AcceptanceCriteria = acceptance };

            // when (act)
            var validationResult = await playAppService.Add(game);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(game);
            Assert.IsFalse(validationResult.IsValid);
        }

    }
}