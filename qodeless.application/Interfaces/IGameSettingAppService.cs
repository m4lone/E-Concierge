using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IGameSettingAppService : IDisposable
    {
        IEnumerable<GameSettingViewModel> GetAll();
        GameSettingViewModel GetById(Guid id);
        IEnumerable<GameSettingViewModel> GetAllBy(Func<GameSetting, bool> exp);
        ValidationResult Add(GameSettingViewModel vm);
        ValidationResult Upsert(GameSettingViewModel vm);
        ValidationResult Update(GameSettingViewModel vm);
        ValidationResult Remove(Guid id);
        GameSettingViewModel GetBy(Func<GameSetting, bool> exp);
    }
}