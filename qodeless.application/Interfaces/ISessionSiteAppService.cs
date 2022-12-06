using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application
{
    public interface ISessionSiteAppService : IDisposable
    {
        IEnumerable<SessionSiteViewModel> GetAll();
        SessionSiteViewModel GetById(Guid id);
        IEnumerable<SessionSiteViewModel> GetAllBy(Func<SessionSite, bool> exp);
        ValidationResult Add(SessionSiteViewModel vm);
        ValidationResult Update(SessionSiteViewModel vm);
        ValidationResult Remove(Guid id);
    }
}