using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class GameViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public EGameCode Code { get; set; }
        public string Name { get; set; }
        public DateTime DtPublish { get; set; }
        public EGameStatus Status { get; set; }
        public string TextoStatus { get; set; }
        public string Version { get; set; }
    }
}





