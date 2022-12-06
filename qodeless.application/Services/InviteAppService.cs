using AutoMapper;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;

namespace qodeless.application
{
    public class InviteAppService : IInviteAppService
    {
        private readonly IMapper _mapper;
        private readonly IInviteRepository _inviteRepository;
        public InviteAppService(IMapper mapper, IInviteRepository inviteRepository)
        {
            _mapper = mapper;
            _inviteRepository = inviteRepository;
        }

        public IEnumerable<InviteViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<InviteViewModel>>(_inviteRepository.GetAll());
        }

        public InviteViewModel GetById(Guid id)
        {
            return _mapper.Map<InviteViewModel>(_inviteRepository.GetById(id));
        }

        public InviteViewModel GetBy(Func<Invite, bool> exp)
        {
            return _mapper.Map<InviteViewModel>(_inviteRepository.GetBy(exp));
        }

        public IEnumerable<InviteViewModel> GetAllBy(Func<Invite, bool> exp)
        {
            return _mapper.Map<IEnumerable<InviteViewModel>>(_inviteRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            var entity = _inviteRepository.GetById(id);
            var validationResult = new InviteRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _inviteRepository.SoftDelete(entity);

            return validationResult;
        }
        public InviteResponseViewModel Add(Guid siteId)
        {
            var entity = new Invite(new Guid()) {
                InviteToken = TokenGenerate(),
                SiteId = siteId,
                DtExpiration = DateTime.Now.AddDays(1),
                IsActive = false
            };
            
            var validationResult = new InviteAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _inviteRepository.Add(entity);

            return new InviteResponseViewModel(validationResult,entity.InviteToken);
        }
        public ValidationResult Update(InviteViewModel vm)
        {
            var entity = _mapper.Map<Invite>(vm);
            var validationResult = new InviteUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _inviteRepository.Update(entity);

            return validationResult;
        }

        //public ValidationResult Upsert(InviteViewModel vm)
        //{
        //    if (Guid.Empty == vm.Id)
        //        return Add(vm);
        //    else
        //        return Update(vm);
        //}
        public string TokenGenerate()
        {
                int Tamanho = 6; // Numero de digitos da senha
                string token = string.Empty;
                for (int i = 0; i < Tamanho; i++)
                {
                    Random random = new Random();
                    int codigo = Convert.ToInt32(random.Next(0,9).ToString());
                    token += codigo.ToString();
                }
                return token;
            }

        public void Dispose() => GC.SuppressFinalize(this);

    }
}
