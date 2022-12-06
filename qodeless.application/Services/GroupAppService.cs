using AutoMapper;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class GroupAppService : IGroupAppService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        public GroupAppService(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public IEnumerable<GroupViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<GroupViewModel>>(_groupRepository.GetAll());
        }

        public GroupViewModel GetById(Guid id)
        {
            return _mapper.Map<GroupViewModel>(_groupRepository.GetById(id));
        }

        public IEnumerable<GroupViewModel> GetAllBy(Func<Group, bool> exp)
        {
            return _mapper.Map<IEnumerable<GroupViewModel>>(_groupRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            var entity = _groupRepository.GetById(id);
            var validationResult = new GroupRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _groupRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(GroupViewModel vm)
        {
            var entity = _mapper.Map<Group>(vm);
            var validationResult = new GroupAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _groupRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(GroupViewModel vm)
        {
            var entity = _mapper.Map<Group>(vm);
            var validationResult = new GroupUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _groupRepository.Update(entity);

            return validationResult;
        }

        public ValidationResult Upsert(GroupViewModel vm)
        {
            if (Guid.Empty == vm.Id)
                return  Add(vm);
            else
                return  Update(vm);
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
