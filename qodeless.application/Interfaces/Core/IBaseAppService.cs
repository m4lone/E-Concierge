using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace qodeless.application.Interfaces.Core
{
    public interface IBaseAppService<TEntity, TViewModel> 
        : IDisposable 
        where TEntity : class 
        where TViewModel : class, new()
    {
        Task<IEnumerable<TViewModel>> GetAll();
        Task<TViewModel> GetById(Guid id);
        Task<IEnumerable<TViewModel>> GetAllBy(Func<TEntity, bool> exp);
        Task<ValidationResult> Add(TViewModel vm);
        Task<ValidationResult> Update(TViewModel vm);
        Task<ValidationResult> Upsert(TViewModel vm);
        Task<ValidationResult> Remove(Guid id);
    }
}
