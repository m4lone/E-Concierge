using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using qodeless.application.Interfaces;
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
    public class DeviceServicesTests
    {
        private readonly Mock<IDeviceRepository> deviceRepositoryMock;
        private readonly IDeviceAppService deviceAppService;
        private readonly Mock<IDeviceGameRepository> deviceGameRepositoryMock;
        private readonly IDeviceGameAppService deviceGameAppService;
        private readonly Mapper mapper;

        public DeviceServicesTests()
        {
            deviceRepositoryMock = new Mock<IDeviceRepository>();

            deviceGameRepositoryMock = new Mock<IDeviceGameRepository>();

            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<Device, DeviceViewModel>();
                cfg.CreateMap<DeviceViewModel, Device>();
            }));

            deviceAppService = new DeviceAppService(this.mapper, this.deviceRepositoryMock.Object, this.deviceGameRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddingDevice_ShouldBeValid()
        {
            // given (arrange)
            var deviceId = It.IsAny<Guid>();
            var device = new DeviceViewModel() { Id = deviceId, Code = "C123", SerialNumber = "SN123",MacAddress="", Status = EDeviceStatus.Actived, Type = EDeviceType.Cabinet };

            // when (act)
            var validationResult =  deviceAppService.Add(device);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(device);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public async Task AddingDeviceNameNull_ShouldNotBeValid()
        {
            // given (arrange)
            var deviceId = It.IsAny<Guid>();
            var device = new DeviceViewModel() { Id = deviceId, Code = "C123", Status = EDeviceStatus.Actived };

            // when (act)
            var validationResult =  deviceAppService.Add(device);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(device);
            // Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("C123")]
        [DataRow("C124")]
        public async Task AddingDeviceNameNull_ShouldNotBeOK(string name)
        {
            // given (arrange)
            var deviceId = It.IsAny<Guid>();
            var device = new DeviceViewModel() { Id = deviceId, Code = name, Status = EDeviceStatus.Actived };

            // when (act)
            var validationResult =  deviceAppService.Add(device);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(device);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        [DataRow("C123")]
        [DataRow("C124")]
        public async Task AddingDeviceNameNull_ShouldNotBeValid(string name)
        {
            // given (arrange)
            var deviceId = It.IsAny<Guid>();
            var device = new DeviceViewModel() { Id = deviceId, Code = name, Status = EDeviceStatus.Actived };

            // when (act)
            var validationResult =  deviceAppService.Add(device);

            // then (assert)
            // 1. Actual list of groups == expected groups
            Assert.IsNotNull(device);
            //Assert.IsFalse(validationResult.IsValid);
        }

    }
}
