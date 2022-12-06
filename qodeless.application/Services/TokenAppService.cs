using AutoMapper;
using FluentValidation.Results;
using NLog;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class TokenAppService : ITokenAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        public TokenAppService(IMapper mapper, ITokenRepository TokenRepository)
        {
            _mapper = mapper;
            _tokenRepository = TokenRepository;
        }

        public async Task<IEnumerable<TokenViewModel>> GetAll()
        {
            Logger.Info("== Token displayed on the index ==");
            return _mapper.Map<IEnumerable<TokenViewModel>>(_tokenRepository.GetAll());
        }

        public async Task<TokenViewModel> GetById(Guid id)
        {
            return _mapper.Map<TokenViewModel>(_tokenRepository.GetById(id));
        }

        public async Task<IEnumerable<TokenViewModel>> GetAllBy(Func<Token, bool> exp)
        {
            return _mapper.Map<IEnumerable<TokenViewModel>>(_tokenRepository.GetAllBy(exp));
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            Logger.Info("== Removing token ==");
            var entity = _tokenRepository.GetById(id);
            var validationResult = new TokenRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _tokenRepository.SoftDelete(entity);

            return validationResult;
        }
        public async Task<ValidationResult> Add(TokenViewModel vm)
        {
            Logger.Info("== Adding token ==");
            var entity = _mapper.Map<Token>(vm);
            var validationResult = new TokenAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _tokenRepository.Add(entity);

            return validationResult;
        }
        public async Task<ValidationResult> Update(TokenViewModel vm)
        {
            Logger.Info("== Updating token ==");
            var entity = _mapper.Map<Token>(vm);
            var validationResult = new TokenUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _tokenRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
