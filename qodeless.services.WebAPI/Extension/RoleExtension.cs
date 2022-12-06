using qodeless.domain.Entities.ACL;

namespace jll.portal_api.services.WebAPI.Extensions
{
    public static class RoleExtension
    {
        public static bool IsValidRole(this string role)
        {
            switch (role)
            {
                case Role.FINANCE_METER:
                case Role.MAINTENANCE:
                case Role.OWNER:
                case Role.PARTNER:
                case Role.SITE_ADMIN:
                case Role.SITE_COUNTER:
                case Role.SITE_OPERATOR:
                case Role.SUPER_ADMIN:
                case Role.PLAYER:
                    return true;
            }
            return false;
        }
    }
}