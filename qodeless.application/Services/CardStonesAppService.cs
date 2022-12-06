using AutoMapper;
using FluentValidation.Results;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.application
{
    public class CardStonesAppService : ICardStonesAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ICardStonesRepository _cardStonesRepository;

        public CardStonesAppService(IMapper mapper, ICardStonesRepository cardStonesRepository)
        {
            _mapper = mapper;
            _cardStonesRepository = cardStonesRepository;
        }

        public IEnumerable<CardStonesViewModel> GetAll()
        {
            Logger.Info("== CardStoness displayed on the index ==");
            return _mapper.Map<IEnumerable<CardStonesViewModel>>(_cardStonesRepository.GetAll());
        }

        public CardStonesViewModel GetById(Guid id)
        {
            var response = _mapper.Map<CardStonesViewModel>(_cardStonesRepository.GetById(id));
            Dispose();
            return response;
        }

        public IEnumerable<CardStonesViewModel> GetAllBy(Func<CardStones, bool> exp)
        {
            return _mapper.Map<IEnumerable<CardStonesViewModel>>(_cardStonesRepository.GetAllBy(exp));
        }

        public CardStonesViewModel GetBy(Func<CardStones, bool> exp)
        {
            return _mapper.Map<CardStonesViewModel>(_cardStonesRepository.GetBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing CardStones ==");
            var entity = _cardStonesRepository.GetById(id);
            var validationResult = new CardStonesRemoveValidator(_cardStonesRepository).Validate(entity);
            if (validationResult.IsValid)
                _cardStonesRepository.SoftDelete(entity);

            return validationResult;
        }

        public ValidationResult Add(CardStonesViewModel vm)
        {
            Logger.Info("== Adding CardStones ==");
            var entity = _mapper.Map<CardStones>(vm);
            var validationResult = new CardStonesAddValidator(_cardStonesRepository).Validate(entity);
            if (validationResult.IsValid)
                _cardStonesRepository.Add(entity);

            return validationResult;
        }

        public ValidationResult Update(CardStonesViewModel vm)
        {
            Logger.Info("== Updating CardStones ==");
            var entity = _mapper.Map<CardStones>(vm);
            var validationResult = new CardStonesAddValidator(_cardStonesRepository).Validate(entity);
            if (validationResult.IsValid)
                _cardStonesRepository.Update(entity);

            return validationResult;
        }

        public string GenerateCardNumber(CardStonesAddViewModel vm)
        {
            var cardNumber = _cardStonesRepository.Any(x => x.GameId == vm.GameId) ?
                _cardStonesRepository.GetAllBy(x => x.GameId == vm.GameId).OrderBy(x => x.CardNumber).LastOrDefault().CardNumber
                : 1;
            var cardStones = new CardStonesViewModel
            {
                Id = vm.Id,
                CardNumber = ++cardNumber,
                StoneNumbersList = vm.StoneNumbersList,
                GameId = vm.GameId
            };
            var entity = _mapper.Map<CardStones>(cardStones);
            var validationResult = new CardStonesAddValidator(_cardStonesRepository).Validate(entity);
            if (validationResult.IsValid)
            {
                _cardStonesRepository.Add(entity);
            }
            return string.Empty;
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
