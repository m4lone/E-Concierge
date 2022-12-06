using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.application
{
    public interface IGroupAppService : IDisposable
    {
        IEnumerable<GroupViewModel> GetAll();
        GroupViewModel GetById(Guid id);
        IEnumerable<GroupViewModel> GetAllBy(Func<Group, bool> exp);
        ValidationResult Add(GroupViewModel vm);
        ValidationResult Update(GroupViewModel vm);
        ValidationResult Upsert(GroupViewModel vm);
        ValidationResult Remove(Guid id);
    }
}