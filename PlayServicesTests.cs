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
    public class PlayServicesTests
    {
        private readonly Mock<IPlayRepository> playRepositoryMock;
        private readonly IPlayAppService playAppService;
        private readonly Mapper mapper;

        public PlayServicesTests()
        {
            playRepositoryMock = new Mock<IPlayRepository>();

            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<Play, PlayViewModel>();
                cfg.CreateMap<PlayViewModel, Play>();
            }));

            playAppService = new PlayAppService(this.mapper, this.playRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddingPlayWithCode1Digit_ShouldNotBeValid()
        {
            // given (arrange)
            var playId = It.IsAny<Guid>();
            var play = new PlayViewModel() { Id = playId, Name = "TESTE", Code = "1", AcceptanceCriteria = 51 };

            // when (act)
            var validationResult = await playAppService.Add(play);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(play);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public async Task GetPlayById_ShouldReturnFieldName()
        {
            // given (arrange)
            var entityId = It.IsAny<Guid>();
            var entityName = It.IsAny<string>(); /*string aleatórias*/
            var play = new Play(entityId) { Name = entityName };
            PlayRepositoryMock.Setup(repository => repository.GetById(entityId)).Returns(group).Verifiable();

            // when (act)
            var playFromService = await playAppService.GetById(entityId);

            // then (assert)
            // 1. Actual list of groups == expected groups
            playRepositoryMock.Verify();
            Assert.IsNotNull(play);
            Assert.AreEqual(entityName, playsFromService.Name);
        }

        [TestMethod]
        public async Task AddingPlayWithAcceptanceCriteriaLow_ShouldNotBeValid()
        {
            // given (arrange)
            var playId = It.IsAny<Guid>();
            var play = new PlayViewModel() { Id = playId, Name = "TESTE", Code = "C123", AcceptanceCriteria = 49 };

            // when (act)
            var validationResult = await playAppService.Add(play);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(play);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("TESTE1", "PLAY", 51)]
        [DataRow("TESTE2", "PLAY", 88)]
        public async Task AddingPlayFullFilled_ShouldBeOK(string code, string name, int acceptance)
        {
            // given (arrange)
            var playId = It.IsAny<Guid>();
            var play = new PlayViewModel() { Id = playId, Name = name, Code = code, AcceptanceCriteria = acceptance };

            // when (act)
            var validationResult = await playAppService.Add(play);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(play);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow(null, "PLAY", 50)]
        [DataRow("", "PLAY", 50)]
        public async Task AddingPlayWithCodeNull_ShouldNotBeValid(string code, string name, int acceptance)
        {
            // given (arrange)
            var playId = It.IsAny<Guid>();
            var play = new PlayViewModel() { Id = playId, Name = name, Code = code, AcceptanceCriteria = acceptance };

            // when (act)
            var validationResult = await playAppService.Add(play);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(play);
            Assert.IsFalse(validationResult.IsValid);
        }

    }
}