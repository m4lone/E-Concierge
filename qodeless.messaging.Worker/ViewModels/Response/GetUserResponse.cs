using qodeless.domain.Enums;
using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class GetUserResponse : BaseResponse
    {
        public string AspNetUserId { get; set; }
        public string Phone { get; set; }
        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string PixKey { get; set; }
        public string Email { get; set; }
        public EGender Gender { get; set; }
    }
}
