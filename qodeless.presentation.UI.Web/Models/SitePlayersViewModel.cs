using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using System.Collections.Generic;

namespace qodeless.presentation.WebApp.Models
{
    public class SitePlayersViewModel
    {
        public string SiteName { get; set; }
        public List<Play> Plays { get; set; } = new List<Play>();
    }
}
