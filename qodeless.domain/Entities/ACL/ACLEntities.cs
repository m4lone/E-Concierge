namespace qodeless.domain.Entities.ACL
{

    #region ACL_IDENTITY_ENTITIES
    public class Role
    {
        public const string SUPER_ADMIN = "SUPER ADMIN";
        public const string PARTNER = "PARTNER";
        public const string OWNER = "OWNER";
        public const string SITE_ADMIN = "SITE ADMIN";
        public const string SITE_OPERATOR = "SITE OPERATOR";
        public const string SITE_COUNTER = "SITE COUNTER";
        public const string MAINTENANCE = "MAINTENANCE";
        public const string FINANCE_METER = "FINANCEMETER";
        public const string PLAYER = "PLAYER";
    }
    #endregion //ACL_IDENTITY_ENTITIES

    #region CLAIM_DEFAULT_VALUES
    public static class ClaimType
    {
        public const string PLAY = "PLAY";
        public const string DEVICE = "DEVICE";
        public const string SESSION_DEVICE = "SESSION DEVICE";
        public const string SESSION_SITE = "SESSION SITE";
        public const string SITE = "SITE";
        public const string ACCOUNT = "ACCOUNT";
        public const string VOUSCHER = "VOUSCHER";
        public const string ACCOUNT_GAME = "ACCOUNT GAME";
        public const string GAME = "GAME";
        public const string DEVICE_GAME = "DEVICE GAME";
        public const string EXPENSE_REQUEST = "EXPENSE REQUEST";
        public const string EXPENSE = "EXPENSE";
        public const string CURRENCY = "CURRENCY";
        public const string INCOME_TYPE = "INCOME TYPE";
        public const string SITE_GAME = "SITE GAME";
        public const string SUCCESS_FEE = "SUCCESS FEE";
        public const string INCOME = "INCOME";
        public const string SITE_PLAYER = "SITE PLAYER";

    }

    public static class ClaimValue
    {
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
        public const string READ = "READ";
    }

    public enum EClaimStatus
    {
        Yes = 1,
        No
    }

    public static class Policy
    {
        public const string PLC01 = "PLC01";
        public const string PLC02 = "PLC02";
        public const string PLC03 = "PLC03";
        public const string PLC04 = "PLC04";
        public const string PLC05 = "PLC05";
        public const string PLC06 = "PLC06";
        public const string PLC07 = "PLC07";
        public const string PLC08 = "PLC08";
        public const string PLC09 = "PLC09";
        public const string PLC10 = "PLC10";
        public const string PLC11 = "PLC11";
        public const string PLC12 = "PLC12";
        public const string PLC13 = "PLC13";
        public const string PLC14 = "PLC14";
        public const string PLC15 = "PLC15";
        public const string PLC16 = "PLC16";
        public const string PLC17 = "PLC17";
        public const string PLC18 = "PLC18";
        public const string PLC19 = "PLC19";
        public const string PLC20 = "PLC20";
        public const string PLC21 = "PLC21";
        public const string PLC22 = "PLC22";
        public const string PLC23 = "PLC23";
        public const string PLC24 = "PLC24";
        public const string PLC25 = "PLC25";
        public const string PLC26 = "PLC26";
        public const string PLC27 = "PLC27";
        public const string PLC28 = "PLC28";
        public const string PLC29 = "PLC29";
        public const string PLC30 = "PLC30";
        public const string PLC31 = "PLC31";
        public const string PLC32 = "PLC32";
        public const string PLC33 = "PLC33";
        public const string PLC34 = "PLC34";
        public const string PLC35 = "PLC35";
        public const string PLC36 = "PLC36";
        public const string PLC37 = "PLC37";
        public const string PLC38 = "PLC38";
        public const string PLC39 = "PLC39";
        public const string PLC40 = "PLC40";
        public const string PLC41 = "PLC41";
        public const string PLC42 = "PLC42";
        public const string PLC43 = "PLC43";
        public const string PLC44 = "PLC44";
        public const string PLC45 = "PLC45";
        public const string PLC46 = "PLC46";
        public const string PLC47 = "PLC47";
        public const string PLC48 = "PLC48";
        public const string PLC49 = "PLC49";
        public const string PLC50 = "PLC50";
        public const string PLC51 = "PLC51";
        public const string PLC52 = "PLC52";
        public const string PLC53 = "PLC53";
        public const string PLC54 = "PLC54";
        public const string PLC55 = "PLC55";
        public const string PLC56 = "PLC56";
        public const string PLC57 = "PLC57";
        public const string PLC58 = "PLC58";
        public const string PLC59 = "PLC59";
        public const string PLC60 = "PLC60";
        public const string PLC61 = "PLC61";
        public const string PLC62 = "PLC62";
        public const string PLC63 = "PLC63";
        public const string PLC64 = "PLC64";
        public const string PLC65 = "PLC65";
        public const string PLC66 = "PLC66";
        public const string PLC67 = "PLC67";
        public const string PLC68 = "PLC68";
        public const string PLC69 = "PLC69";
        public const string PLC70 = "PLC70";
        public const string PLC71 = "PLC71";
        public const string PLC72 = "PLC72";
        public const string PLC73 = "PLC73";
        public const string PLC74 = "PLC74";
        public const string PLC75 = "PLC75";
        public const string PLC76 = "PLC76";
        public const string PLC77 = "PLC77";
        public const string PLC78 = "PLC78";
        public const string PLC79 = "PLC79";
        public const string PLC80 = "PLC80";
        public const string PLC81 = "PLC81";
        public const string PLC82 = "PLC82";
        public const string PLC83 = "PLC83";
        public const string PLC84 = "PLC84";
        public const string PLC85 = "PLC85";
        public const string PLC86 = "PLC86";
        public const string PLC87 = "PLC87";
        public const string PLC88 = "PLC88";
        public const string PLC89 = "PLC89";
        public const string PLC90 = "PLC90";
        public const string PLC91 = "PLC91";
        public const string PLC92 = "PLC92";
        public const string PLC93 = "PLC93";
        public const string PLC94 = "PLC94";
        public const string PLC95 = "PLC95";
        public const string PLC96 = "PLC96";
        public const string PLC97 = "PLC97";
        public const string PLC98 = "PLC98";
        public const string PLC99 = "PLC99";
        public const string PLC100 = "PLC100";
        public const string PLC101 = "PLC101";
        public const string PLC102 = "PLC102";
        public const string PLC103 = "PLC103";
        public const string PLC104 = "PLC104";
        public const string PLC105 = "PLC105";
        public const string PLC106 = "PLC106";
        public const string PLC107 = "PLC107";
        public const string PLC108 = "PLC108";
        public const string PLC109 = "PLC109";
        public const string PLC110 = "PLC110";
        public const string PLC111 = "PLC111";
        public const string PLC112 = "PLC112";
        public const string PLC113 = "PLC113";
        public const string PLC114 = "PLC114";
        public const string PLC115 = "PLC115";
        public const string PLC116 = "PLC116";
        public const string PLC117 = "PLC117";
        public const string PLC118 = "PLC118";
        public const string PLC119 = "PLC119";
        public const string PLC120 = "PLC120";
        public const string PLC121 = "PLC121";
        public const string PLC122 = "PLC122";
        public const string PLC123 = "PLC123";
        public const string PLC124 = "PLC124";
        public const string PLC125 = "PLC125";
        public const string PLC126 = "PLC126";
        public const string PLC127 = "PLC127";
        public const string PLC128 = "PLC128";
        public const string PLC129 = "PLC129";
        public const string PLC130 = "PLC130";
        public const string PLC131 = "PLC131";
        public const string PLC132 = "PLC132";
        public const string PLC133 = "PLC133";
        public const string PLC134 = "PLC134";
        public const string PLC135 = "PLC135";
        public const string PLC136 = "PLC136";
        public const string PLC137 = "PLC137";
        public const string PLC138 = "PLC138";
        public const string PLC139 = "PLC139";
        public const string PLC140 = "PLC140";
        public const string PLC141 = "PLC141";
        public const string PLC142 = "PLC142";
        public const string PLC143 = "PLC143";
        public const string PLC144 = "PLC144";
    }
    #endregion //CLAIM_DEFAULT_VALUES
}
