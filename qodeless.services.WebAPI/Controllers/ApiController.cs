using FluentValidation.Results;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Entities.ACL;
using qodeless.domain.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.DataModel;
using qodeless.services.WebApi.Hubs;
using qodeless.services.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    public class ApiController : ControllerBase
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        protected readonly ApplicationDbContext Db;
        protected readonly IHubContext<QodelessHub> _hubContext;
        protected readonly IWebHostEnvironment _env;

        public ApiController(ApplicationDbContext db,
             IHubContext<QodelessHub> hubContext = null,
            IWebHostEnvironment env = null)

        {
            _env = env;
            _hubContext = hubContext;
            Db = db;
        }
       public static void MainApi(string[] args)
        {
            Logger.Info("==== API has started ==== ");
           
        }

        protected async Task LockGameHub()
        {
            await _hubContext.Clients.All
                .SendAsync("LockGameHub", new QodelessHub.Message()
                {
                    Text = "Lock",
                    UserName = "Eliel"
                });
        }

        protected async Task UnlockGameHub()
        {
            await _hubContext.Clients.All
                .SendAsync("UnlockGameHub", new QodelessHub.Message()
                {
                    Text = "Unlock",
                    UserName = "Eliel"
                });
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
        protected IUserDataModel GetLoggedUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst("/email")?.Value;

            return GetUser(email);
        }
        private readonly ICollection<string> _errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid()) {
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
            foreach (var error in modelState.Values.SelectMany(c=>c.Errors))
            {
                AddError(error.ErrorMessage);
            }
            return CustomResponse(new { success = true});
        }
        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.PropertyName,error.ErrorMessage);
            }
            return CustomResponse(new { success = true });
        }
        protected bool IsOperationValid() => !_errors.Any();
        protected void AddError(string field, string erro) => _errors.Add($"{field}|{erro}");
        protected void AddError(string erro) => _errors.Add(erro);
        protected void ClearErrors() => _errors.Clear();

        // CONSTANTES PARA ENVIAR E-MAIL
        public const string APP_KEY = "489f4dea806321fc159c8b9ad81acc02";
        public const string APP_SECRET = "9fa3ebab9eaa3ae0eda308bd45a73b42";
        public const string MAIL_DEFAULT = "luan.oliveira@qodeless.io";
        public static bool SendMail(Guid userId, string to, string subject, string body, string fromEmail = MAIL_DEFAULT, string cc = "", string bcc = "")
        {
            try
            {
                var client = new MailjetClient(APP_KEY, APP_SECRET);
                var request = new MailjetRequest { Resource = Send.Resource, };

                var email = new TransactionalEmailBuilder()
                    .WithFrom(new SendContact(fromEmail))
                    .WithSubject(subject)
                    .WithHtmlPart(body);

                if (!string.IsNullOrEmpty(cc))
                    email.WithCc(new SendContact(cc));

                if (!string.IsNullOrEmpty(bcc))
                    email.WithBcc(new SendContact(bcc));


                var emailContent = email
                    .WithTo(new SendContact(to))
                    .Build();

                // invoke API to send email
                var response = client.SendTransactionalEmailAsync(emailContent).Result;

                return !response.Messages.Any(_ => _.Status != "success");
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        #region MAIN_QUERIES

        protected GamePlayerSummary GetGamesPlayerById(string userId, Guid siteId)
        {
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"INICIO QUERY GamePlayerSummary: {DateTime.Now.ToLongTimeString()}");
            var playerSummary = (
                from user in Db.Users where user.Id == userId
                join userRole in Db.UserRoles on user.Id equals userRole.UserId
                join role in Db.Roles on userRole.RoleId equals role.Id
                join sitePlayer in Db.SitePlayers on user.Id equals sitePlayer.UserPlayerId
                join site in Db.Sites on sitePlayer.SiteId equals site.Id
                join account in Db.Accounts on site.AccountId equals account.Id
                where user.Id == userId
                where site.Id == siteId
                where sitePlayer.SiteId == siteId
                where userRole.UserId == userId
                where userRole.RoleId == role.Id
                select new 
                {
                    AccountId = site.AccountId,
                    AccountStatus = account.Status,
                    IsUserBlocked = user.LockoutEnabled,
                    SiteId = site.Id,                    
                    RoleId = role.Id,
                    Role = role.Name
                }
            ).FirstOrDefault();

            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"FIM PRIMEIRA PARTE QUERY GamePlayerSummary: {DateTime.Now.ToLongTimeString()}");
            Console.WriteLine($"INICIO PARTE DeviceSummary DA QUERY GamePlayerSummary: {DateTime.Now.ToLongTimeString()}");
            var deviceSummary = (
                from user in Db.Users
                join userRole in Db.UserRoles on user.Id equals userRole.UserId
                join role in Db.Roles on userRole.RoleId equals role.Id 
                join sitePlayer in Db.SitePlayers on user.Id equals sitePlayer.UserPlayerId
                join site in Db.Sites on sitePlayer.SiteId equals site.Id
                join siteDevice in Db.SiteDevices on site.Id equals siteDevice.SiteId
                join device in Db.Devices on siteDevice.DeviceId equals device.Id
                join deviceGame in Db.DeviceGames on device.Id equals deviceGame.DeviceId
                join game in Db.Games on deviceGame.GameId equals game.Id
                join siteGame in Db.SiteGames on game.Id equals siteGame.GameId 
                where user.Id == userId
                where site.Id == siteId
                where userRole.UserId == userId
                where siteDevice.SiteId == siteId
                where siteGame.SiteId == siteId
                where siteDevice.SiteId == site.Id
                where siteDevice.DeviceId == device.Id
                where deviceGame.GameId == game.Id
                where deviceGame.DeviceId == device.Id
                where siteGame.SiteId == site.Id
                where siteGame.GameId == game.Id

                select new GamePlayerSummary.Device()
                {
                    Id = device.Id,
                    SerialNumber = device.SerialNumber,
                    GameId = game.Id,
                    GameName = game.Name,
                    GameType = game.Type   
                }
            ).ToList();
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX UBUNTU");
            Console.WriteLine($"FIM DA QUERY GamePlayerSummary: {DateTime.Now.ToLongTimeString()}");

            //TODO: corrigir amount fixo
            var result = new GamePlayerSummary
            {
                Devices = deviceSummary.OrderBy(x => x.SerialNumber).ToList(),                                
            };

            if (playerSummary != null)
            {
                result.SiteId = playerSummary.SiteId;
                result.AccountId = playerSummary.AccountId;
                result.AccountStatus = playerSummary.AccountStatus;
                result.IsUserBlocked = playerSummary.IsUserBlocked;
            }

            return result;
        }
        protected List<RecSitesUnityResponseVM> GetSiteByUserId(string userId)
        {
            return (
                from sitePLayer in Db.SitePlayers
                join site in Db.Sites on sitePLayer.SiteId equals site.Id
                where sitePLayer.UserPlayerId == userId
                select new RecSitesUnityResponseVM()
                {
                    AccountId = site.AccountId,
                    Id = site.Id,
                    LastAccessAt = sitePLayer.UpdatedAt,
                    CreditAmount = sitePLayer.CreditAmount,
                    Name = site.Name,
                    Code = site.Code
                }
            ).OrderByDescending(x => x.CreditAmount).ThenByDescending(x => x.LastAccessAt).Take(3).ToList(); 
        }
        protected IUserDataModel GetPlayerById(string userId)
        {
            var userDataModel = (
                from user in Db.Users
                join userRole in Db.UserRoles on user.Id equals userRole.UserId
                join role in Db.Roles on userRole.RoleId equals role.Id
                where user.Id == userId
                select new UserDataModel()
                {
                    RoleId = role.Id,
                    Role = role.Name,
                    Email = user.Email,
                    UserId = user.Id,
                    UserName = user.UserName,
                }
            ).FirstOrDefault();

            return userDataModel;

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
                    UserName = role.Name == Role.PARTNER ?
                        Db.Accounts.Where(x => x.UserId.Equals(user.Id)).Select(x => x.Name).FirstOrDefault() : (role.Name == Role.SITE_ADMIN ? Db.Sites.Where(x => x.UserId.Equals(user.Id)).Select(x => x.Name).FirstOrDefault() : "RG Digital"),
                    SiteId = role.Name == Role.SITE_ADMIN ? 
                        Db.Sites.Where(x => x.UserId.Equals(user.Id)).Select(x => x.Id).FirstOrDefault() : Guid.Empty,
                    AccountId = role.Name == Role.PARTNER ? 
                        Db.Accounts.Where(x => x.UserId.Equals(user.Id)).Select(x => x.Id).FirstOrDefault() : (role.Name == Role.SITE_ADMIN ? Db.Sites.Where(x => x.UserId.Equals(user.Id)).Select(x => x.AccountId).FirstOrDefault() : Guid.Empty),
                }
            ).FirstOrDefault();

            if (response != null)
                response.Claims = GetDefaultClaims(response.RoleId, response);

            return response;

        }

        #endregion //MAIN_QUERIES
    }
}
