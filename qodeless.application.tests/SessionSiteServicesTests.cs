using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace qodeless.application.tests
{
    [TestClass]
    public class SessionSiteServicesTests
    {
        private readonly Mock<ISessionSiteRepository>SessionSiteRepositoryMock;
        private readonly ISessionSiteAppService SessionSiteAppService;
        private readonly Mapper mapper;

        public SessionSiteServicesTests()
        {
           SessionSiteRepositoryMock = new Mock<ISessionSiteRepository>();

            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<SessionSite, SessionSiteViewModel>();
                cfg.CreateMap<SessionSiteViewModel, SessionSite>();
            }));

           SessionSiteAppService = new SessionSiteAppService(this.mapper, this.SessionSiteRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddingSessionSite_ShouldBeValid()
        {
            // given (arrange)
            var SessionSiteId = It.IsAny<Guid>();
            var SessionSite = new SessionSiteViewModel() { Id =SessionSiteId, UserOperationId = "TESTE", DtBegin = DateTime.Now, DtEnd = DateTime.Now, Status = EStatusSessionSite.BusinessDayOpen};

            // when (act)
            var validationResult =  SessionSiteAppService.Add(SessionSite);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionSite);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public async Task GetSessionSiteById_ShouldReturnFieldName()
        {
            // given (arrange)
            var entityId = It.IsAny<Guid>();
            var entityName = It.IsAny<string>(); /*string aleatórias*/
            var SessionSite = new SessionSite(entityId) { UserOperationId = entityName };
           SessionSiteRepositoryMock.Setup(repository => repository.GetById(entityId)).Returns(SessionSite).Verifiable();

            // when (act)
            var SessionSitesFromService =  SessionSiteAppService.GetById(entityId);

            // then (assert)
            // 1. Actual list of groups == expected groups
           SessionSiteRepositoryMock.Verify();
            Assert.IsNotNull(SessionSite);
            Assert.AreEqual(entityName,SessionSitesFromService.UserOperationId);
        }

        [TestMethod]
        public async Task AddingSessionSiteNameNull_ShouldNotBeValid()
        {
            // given (arrange)
            var SessionSiteId = It.IsAny<Guid>();
            var SessionSite = new SessionSiteViewModel() { Id =SessionSiteId, UserOperationId = "TESTE", DtBegin = DateTime.Now, DtEnd = DateTime.Now, Status = EStatusSessionSite.BusinessDayOpen};

            // when (act)
            var validationResult =  SessionSiteAppService.Add(SessionSite);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionSite);
            // Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("TESTE1")]
        [DataRow("TESTE2")]
        public async Task AddingSessionSiteFullFilled_ShouldBeOK(string useroperationId)
        {
            // given (arrange)
            var SessionSiteId = It.IsAny<Guid>();
            var SessionSite = new SessionSiteViewModel() { Id =SessionSiteId, UserOperationId = useroperationId, DtBegin =  DateTime.Now, DtEnd = DateTime.Now, Status =  EStatusSessionSite.BusinessDayOpen};

            // when (act)
            var validationResult =  SessionSiteAppService.Add(SessionSite);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionSite);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("TESTE1")]
        [DataRow("TESTE2")]
        public async Task AddingSessionSiteNameNull_ShouldNotBeValid(string useroperationId)
        {
            // given (arrange)
            var SessionSiteId = It.IsAny<Guid>();
            var SessionSite = new SessionSiteViewModel() { Id =SessionSiteId, UserOperationId = useroperationId, DtBegin = DateTime.Now, DtEnd = DateTime.Now, Status = EStatusSessionSite.BusinessDayOpen};

            // when (act)
            var validationResult =  SessionSiteAppService.Add(SessionSite);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionSite);
            //Assert.IsFalse(validationResult.IsValid);
        }

    }
}
