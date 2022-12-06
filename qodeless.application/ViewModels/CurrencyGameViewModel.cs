using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.ViewModels
{
    public class CurrencyGameViewModel
    {
        public Guid SiteId { get; set; }
        public List<EGameCurrency> GameCurrencies { get; set; }
    }
}
