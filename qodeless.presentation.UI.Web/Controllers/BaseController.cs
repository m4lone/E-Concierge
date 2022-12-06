using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using qodeless.domain.Entities;
using qodeless.domain.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.DataModel;
using qodeless.Infra.CrossCutting.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace qodeless.services.WebApp.Controllers
{
    public abstract class BaseController<TViewModel> : Controller where TViewModel : class, new()
    {
        private readonly ApplicationDbContext Db;
        public BaseController(ApplicationDbContext db)
        {
            Db = db;
        }

        public abstract void LoadViewBags();
        public abstract IEnumerable<TViewModel> GetRows();
        public abstract TViewModel GetRow(Guid id);
        protected void NotifyOperation(OperationResult result, string title, string message)
        {
            if (result == OperationResult.Sucess)
            {
                ViewBag.Sucess = true;
                ModelState.Clear();
            }
            else if (result == OperationResult.Error)
                ViewBag.Error = true;

            ViewBag.MessageTitle = title;
            ViewBag.Message = message;
        }

        protected enum OperationResult
        {
            Sucess,
            Error
        }


        protected List<IdentityRoleClaim<string>> GetClaims(string roleId)
        {
            return Db.RoleClaims.Where(_ => _.RoleId == roleId).ToList();
        }

        protected List<ClaimViewModel> GetDefaultClaims(string roleId, IUserDataModel user)
        {
            var result = new List<ClaimViewModel>();
            var roleClaims = Db.RoleClaims.Where(_ => _.RoleId == roleId).ToList();
            var userClaims = Db.UserClaims.Where(_ => _.UserId == user.UserId).ToList();

            if (userClaims != null && userClaims.Count > 0)
            {
                //Adiciona claims relacionados ao User sem repetir o que ja foi adicionado anteriormente
                foreach (var subItem in userClaims.Where(_ => !result.Any(r => r.ClaimType.ToLower() == _.ClaimType.ToLower() && r.ClaimValue.ToLower() == _.ClaimValue.ToLower())))
                {
                    result.Add(new ClaimViewModel { ClaimType = subItem.ClaimType, ClaimValue = subItem.ClaimValue });
                }
                return result;
            }

            if (roleClaims != null && roleClaims.Count > 0)
            {
                //Adiciona claims relacionados ao Role
                foreach (var item in roleClaims)
                {
                    result.Add(new ClaimViewModel { ClaimType = item.ClaimType, ClaimValue = item.ClaimValue });
                }
            }

            return result;
        }
        protected List<UserDataModel> GetUsers()
        {
            return Db.Users.Select(x => new UserDataModel { UserName = x.UserName, Email = x.Email, UserId = x.Id}).ToList();
        }
        protected IUserDataModel GetUser(string email)
        {
            var response = (
                from user in Db.Users
                join userRole in Db.UserRoles on user.Id equals userRole.UserId
                join role in Db.Roles on userRole.RoleId equals role.Id
                where user.Email.ToLower() == email.ToLower()
                select new UserDataModel()
                {
                    RoleId = role.Id,
                    Role = role.Name,
                    Email = user.Email,
                    UserId = user.Id,
                    UserName = user.UserName,
                }
            ).FirstOrDefault();

            if (response != null)
                response.Claims = GetDefaultClaims(response.RoleId, response);

            return response;

        }
        protected new IActionResult Response(object result = null, bool success = true, string errorMessage = "")
        {
            if (success)
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = errorMessage
            });
        }
        protected new IActionResult ModelStateError()
        {
            var result = new List<string>();
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                result.Add(erroMsg);
            }

            return Response(success: false, errorMessage: string.Join("\r\n", result));
        }
        protected IActionResult ViewDefault(string action, TViewModel vm, ValidationResult validationResult)
        {
            LoadViewBags();
            if (!ModelState.IsValid)
                return View(vm);

            if (validationResult.Errors.Count > 0)
            {
                foreach (var error in validationResult.Errors)
                {
                    NotifyOperation(OperationResult.Error, error.PropertyName, error.ErrorMessage);
                    AddError(error.PropertyName, error.ErrorMessage);
                }
                return View(vm);
            }
            else
            {
                return RedirectToAction(action);
            }
        }
        protected IActionResult ViewForm(Guid id)
        {
            LoadViewBags();

            if (id == Guid.Empty)
                return View(new TViewModel());
            else
                return View(GetRow(id));
        }
        protected IUserDataModel GetLoggedUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst("/email")?.Value;

            return GetUser(email);
        }
        private readonly ICollection<string> _errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {
                    "ErrorMessages",
                    _errors.ToArray()
                }
            }));
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            foreach (var error in modelState.Values.SelectMany(c => c.Errors))
            {
                AddError(error.ErrorMessage);
            }
            return CustomResponse(new { success = true });
        }
        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.PropertyName, error.ErrorMessage);
            }
            return CustomResponse(new { success = true });
        }
        protected bool IsOperationValid() => !_errors.Any();
        protected void AddError(string field, string erro) => _errors.Add($"{field}|{erro}");
        protected void AddError(string erro) => _errors.Add(erro);
        protected void ClearErrors() => _errors.Clear();

    }
}
