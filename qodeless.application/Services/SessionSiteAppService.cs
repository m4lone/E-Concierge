using AutoMapper;
using FluentValidation.Results;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;

namespace qodeless.application
{
    public class SessionSiteAppService : ISessionSiteAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISessionSiteRepository _sessionSiteRepository;
        public SessionSiteAppService(IMapper mapper, ISessionSiteRepository SessionSiteRepository)
        {
            _mapper = mapper;
            _sessionSiteRepository = SessionSiteRepository;
        }

        public IEnumerable<SessionSiteViewModel> GetAll()
        {
            Logger.Info("== SessionSite displayed on the index ==");
            return _mapper.Map<IEnumerable<SessionSiteViewModel>>(_sessionSiteRepository.GetAll());
        }

        public SessionSiteViewModel GetById(Guid id)
        {
            return _mapper.Map<SessionSiteViewModel>(_sessionSiteRepository.GetById(id));
        }

        public IEnumerable<SessionSiteViewModel> GetAllBy(Func<SessionSite, bool> exp)
        {
            return _mapper.Map<IEnumerable<SessionSiteViewModel>>(_sessionSiteRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing session site ==");
            var entity = _sessionSiteRepository.GetById(id);
            var validationResult = new SessionSiteRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _sessionSiteRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SessionSiteViewModel vm)
        {
            Logger.Info("== Adding session site ==");
            var entity = _mapper.Map<SessionSite>(vm);
            entity.DtEnd = DateTime.MinValue;
            var validationResult = new SessionSiteAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _sessionSiteRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SessionSiteViewModel vm)
        {
            Logger.Info("== Updating session site ==");
            var entity = _mapper.Map<SessionSite>(vm);
            var validationResult = new SessionSiteUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _sessionSiteRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
