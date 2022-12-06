using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.application
{
    public interface IInviteAppService : IDisposable
    {
        IEnumerable<InviteViewModel> GetAll();
        InviteViewModel GetById(Guid id);
        IEnumerable<InviteViewModel> GetAllBy(Func<Invite, bool> exp);
        string TokenGenerate();
        InviteResponseViewModel Add(Guid siteId);
        ValidationResult Update(InviteViewModel vm);
        //ValidationResult Upsert(InviteViewModel vm);
        ValidationResult Remove(Guid id);
        InviteViewModel GetBy(Func<Invite, bool> exp);
    }
}