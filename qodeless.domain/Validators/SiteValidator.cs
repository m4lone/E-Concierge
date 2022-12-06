using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;
using System.Text.RegularExpressions;

namespace qodeless.domain.Validators
{
    public class SiteAddValidator : SiteValidatorBase
    {
        public SiteAddValidator(ISiteRepository siteRepository) 
            :base(siteRepository)
        {
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).Must(AlreadyExists).WithMessage("Registro já existente");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.Code)
            //   .Matches(new Regex(@"^RG[0-9]{4}(?:[A-Z0-9]{2})$"))
            //   .WithMessage("Campo Code Inválido");
            RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            RuleFor(entity => entity.Address)
                .NotNull()
                .When(entity => !string.IsNullOrEmpty(entity.City))
                .WithMessage("Endereço Inválido");
        }
    }

    public class SiteUpdateValidator : SiteValidatorBase
    {
        public SiteUpdateValidator(ISiteRepository siteRepository)
            : base(siteRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.Code)
            //   .Matches(new Regex(@"^RG[0-9]{4}(?:[A-Z0-9]{2})$"))
            //   .WithMessage("Campo Code Inválido");
            RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
        }
    }

    public class SiteRemoveValidator : SiteValidatorBase
    {
        public SiteRemoveValidator(ISiteRepository siteRepository)
            : base(siteRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public abstract class SiteValidatorBase : AbstractValidator<Site>
    {
        protected readonly ISiteRepository _siteRepository;
        public SiteValidatorBase(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        protected bool IsValidAccount(Guid accountId) => _siteRepository.IsValidAccountId(accountId);
      
        protected bool AlreadyExists(string name) => _siteRepository.None(site => site.Name == name);
    }        
}
