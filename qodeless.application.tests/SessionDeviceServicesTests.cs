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
    public class SessionDeviceServicesTests
    {
        private readonly Mock<ISessionDeviceRepository> SessionDeviceRepositoryMock;
        private readonly ISessionDeviceAppService SessionDeviceAppService;
        private readonly Mapper mapper;

        public SessionDeviceServicesTests()
        {
            SessionDeviceRepositoryMock = new Mock<ISessionDeviceRepository>();

            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<SessionDevice, SessionDeviceViewModel>();
                cfg.CreateMap<SessionDeviceViewModel, SessionDevice>();
            }));

            SessionDeviceAppService = new SessionDeviceAppService(this.mapper, this.SessionDeviceRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddingSessionSite_ShouldBeValid()
        {
            // given (arrange)
            var SessionDeviceId = It.IsAny<Guid>();
            var SessionDevice = new SessionDeviceViewModel() { Id = SessionDeviceId, UserPlayId = "TESTE", LastIpAddress = "TESTE", DtBegin = DateTime.Now, DtEnd = DateTime.Now};

            // when (act)
            var validationResult =  SessionDeviceAppService.Add(SessionDevice);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionDevice);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public async Task GetSessionDeviceById_ShouldReturnFieldName()
        {
            // given (arrange)
            var entityId = It.IsAny<Guid>();
            var entityName = It.IsAny<string>(); /*string aleatórias*/
            var SessionDevice = new SessionDevice(entityId) { UserPlayId = entityName };
            SessionDeviceRepositoryMock.Setup(repository => repository.GetById(entityId)).Returns(SessionDevice).Verifiable();

            // when (act)
            var SessionSitesFromService =  SessionDeviceAppService.GetById(entityId);

            // then (assert)
            // 1. Actual list of groups == expected groups
            SessionDeviceRepositoryMock.Verify();
            Assert.IsNotNull(SessionDevice);
            //Assert.AreEqual(entityName, SessionDevicesFromService.UserPlayId);
        }

        [TestMethod]
        public async Task AddingSessionDeviceNameNull_ShouldNotBeValid()
        {
            // given (arrange)
            var SessionDeviceId = It.IsAny<Guid>();
            var SessionDevice = new SessionDeviceViewModel() { Id = SessionDeviceId, UserPlayId = "TESTE", LastIpAddress = "TESTE", DtBegin = DateTime.Now, DtEnd = DateTime.Now};

            // when (act)
            var validationResult =  SessionDeviceAppService.Add(SessionDevice);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionDevice);
            // Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("TESTE1")]
        [DataRow("TESTE2")]
        public async Task AddingSessionDeviceFullFilled_ShouldBeOK(string userPlayId)
        {
            // given (arrange)
            var SessionDeviceId = It.IsAny<Guid>();
            var SessionDevice = new SessionDeviceViewModel() { Id = SessionDeviceId, UserPlayId = userPlayId, DtBegin =  DateTime.Now, DtEnd = DateTime.Now};

            // when (act)
            var validationResult =  SessionDeviceAppService.Add(SessionDevice);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionDevice);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("TESTE1")]
        [DataRow("TESTE2")]
        public async Task AddingSessionDeviceNameNull_ShouldNotBeValid(string userPlayId)
        {
            // given (arrange)
            var SessionDeviceId = It.IsAny<Guid>();
            var SessionDevice = new SessionDeviceViewModel() { Id = SessionDeviceId, UserPlayId = userPlayId, DtBegin = DateTime.Now, DtEnd = DateTime.Now};

            // when (act)
            var validationResult =  SessionDeviceAppService.Add(SessionDevice);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(SessionDevice);
            //Assert.IsFalse(validationResult.IsValid);
        }

    }
}
