using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using System.Collections.Generic;

namespace qodeless.presentation.WebApp.Models
{
    public class PartnerGamesViewModel
    {
        public string AccountName { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
