using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class GroupViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int AcceptanceCriteria { get; set; }
    }
}
