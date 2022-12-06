using Microsoft.Extensions.DependencyInjection;
using qodeless.DataManager.Seeds;
using qodeless.domain.Entities.ACL;
using System.Linq;

namespace qodeless.services.WebApi.Extension
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, string rootPath)
        {
            var roleClaims = SeederBase.LoadClaimsFromFile(rootPath);

            return services.AddAuthorization(options =>
            {
                foreach (var roleClaim in roleClaims)
                {
                    options.AddPolicy(roleClaim.PolicyName, builder =>
                    {
                        builder.RequireAuthenticatedUser();
                        builder.RequireRole(roleClaim.Role);
                        builder.RequireClaim(roleClaim.Claims.FirstOrDefault().Type.ToUpper(), roleClaim.Claims.Where(_ => _.Status == EClaimStatus.Yes).Select(_ => _.Value.ToUpper()));
                        builder.Build();
                    });
                }
            });
        }
    }
}
