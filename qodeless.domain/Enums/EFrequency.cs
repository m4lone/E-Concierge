using System.ComponentModel;

namespace qodeless.domain.Enums
{
    public enum EFrequency
    {        
        [Description("Diariamente")]
        Daily = 1,
        [Description("Semanalmente")]
        Weekly,
        [Description("Mensalmente")]
        Monthly,
        [Description("Bimestralmente")]
        Bimonthly,
        [Description("Trimestralmente")]
        Quarterly,
        [Description("Semestralmente")]
        Semiannually, 
        [Description("Anualmente")]
        Annually 
    }
}
