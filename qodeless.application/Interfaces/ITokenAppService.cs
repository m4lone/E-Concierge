using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application.Interfaces
{
    public interface ITokenAppService : IDisposable
    {
        Task<IEnumerable<TokenViewModel>> GetAll();
        Task<TokenViewModel> GetById(Guid id);
        Task<IEnumerable<TokenViewModel>> GetAllBy(Func<Token, bool> exp);
        Task<ValidationResult> Add(TokenViewModel vm);
        Task<ValidationResult> Update(TokenViewModel vm);
        Task<ValidationResult> Remove(Guid id);
    }
}
