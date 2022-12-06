//using AutoMapper;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using qodeless.application.ViewModels;
//using qodeless.domain.Entities;
//using qodeless.domain.Interfaces.Repositories;
//using System;
//using System.Threading.Tasks;
//using Tynamix.ObjectFiller;

//namespace qodeless.application.tests
//{
//    [TestClass]
//    public class AccountGameServicesTests
//    {
//        private readonly Mock<IAccountGameRepository> AccountGameRepositoryMock;
//        private readonly IGroupAppService groupAppService;
//        private readonly Mapper mapper;

//        public AccountGameServicesTests()
//        {
//            groupRepositoryMock = new Mock<IGroupRepository>();

//            mapper = new Mapper(new MapperConfiguration(cfg => {
//                cfg.CreateMap<Group, GroupViewModel>();
//                cfg.CreateMap<GroupViewModel, Group>();
//            }));
            
//            groupAppService = new GroupAppService(this.mapper, this.groupRepositoryMock.Object);
//        }

//        [TestMethod]
//        public async Task AddingGroupWithCode1Digit_ShouldNotBeValid()
//        {
//            // given (arrange)
//            var groupId = It.IsAny<Guid>();
//            var group = new GroupViewModel() { Id = groupId, Name = "TESTE", Code = "1", AcceptanceCriteria = 51 };

//            // when (act)
//            var validationResult = await groupAppService.Add(group);

//            // then (assert)
//            // 1. Actual list of groups == expected groups
//            Assert.IsNotNull(group);
//            Assert.IsFalse(validationResult.IsValid);
//        }

//        [TestMethod]
//        public async Task GetGroupById_ShouldReturnFieldName()
//        {
//            // given (arrange)
//            var entityId = It.IsAny<Guid>();
//            var entityName = It.IsAny<string>(); /*string aleatórias*/
//            var group = new Group(entityId) { Name = entityName };
//            groupRepositoryMock.Setup(repository => repository.GetById(entityId)).Returns(group).Verifiable();

//            // when (act)
//            var groupsFromService = await groupAppService.GetById(entityId);

//            // then (assert)
//            // 1. Actual list of groups == expected groups
//            groupRepositoryMock.Verify();
//            Assert.IsNotNull(group);
//            Assert.AreEqual(entityName, groupsFromService.Name);
//        }

//        [TestMethod]
//        public async Task AddingGroupWithAcceptanceCriteriaLow_ShouldNotBeValid()
//        {
//            // given (arrange)
//            var groupId = It.IsAny<Guid>();
//            var group = new GroupViewModel() { Id = groupId, Name = "TESTE", Code = "C123", AcceptanceCriteria = 49 };

//            // when (act)
//            var validationResult = await groupAppService.Add(group);

//            // then (assert)
//            // 1. Actual list of groups == expected groups
//            Assert.IsNotNull(group);
//            Assert.IsFalse(validationResult.IsValid);
//        }

//        [TestMethod]
//        [DataRow("TESTE1", "GROUP", 51)]
//        [DataRow("TESTE2", "GROUP", 88)]
//        public async Task AddingGroupFullFilled_ShouldBeOK(string code, string name, int acceptance)
//        {
//            // given (arrange)
//            var groupId = It.IsAny<Guid>();
//            var group = new GroupViewModel() { Id = groupId, Name = name, Code = code, AcceptanceCriteria = acceptance };

//            // when (act)
//            var validationResult = await groupAppService.Add(group);

//            // then (assert)
//            // 1. Actual list of groups == expected groups
//            Assert.IsNotNull(group);
//            Assert.IsTrue(validationResult.IsValid);
//        }

//        [TestMethod]
//        [DataRow(null, "GROUP", 50)]
//        [DataRow("", "GROUP", 50)]
//        public async Task AddingGroupWithCodeNull_ShouldNotBeValid(string code, string name, int acceptance)
//        {
//            // given (arrange)
//            var groupId = It.IsAny<Guid>();
//            var group = new GroupViewModel() { Id = groupId, Name = name, Code = code, AcceptanceCriteria = acceptance };

//            // when (act)
//            var validationResult = await groupAppService.Add(group);

//            // then (assert)
//            // 1. Actual list of groups == expected groups
//            Assert.IsNotNull(group);
//            Assert.IsFalse(validationResult.IsValid);
//        }
 
//    }
//}
