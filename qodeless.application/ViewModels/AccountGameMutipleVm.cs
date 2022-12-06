using System;
using System.Collections.Generic;

namespace qodeless.application.ViewModels
{
    public class AccountGameMutiplevm
    {
        public Guid Account { get; set; }
        public List<Guid> GameIds { get; set; }
    }
}
