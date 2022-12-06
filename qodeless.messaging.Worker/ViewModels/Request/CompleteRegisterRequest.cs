using qodeless.domain.Enums;
using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class CompleteRegisterRequest
    {
        public string Phone { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string PixKey { get; set; }
        public string Email { get; set; }
        public EGender Gender { get; set; }
    }
}
