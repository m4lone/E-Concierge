using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class RegisterRequest
    {
        public RegisterRequest(string phone, string inviteToken, string password, string nickName, DateTime birthDate)
        {
            Phone = phone;
            InviteToken = inviteToken;
            Password = password;
            NickName = nickName;
            BirthDate = birthDate;
        }

        public string Phone { get; set; }
        public string InviteToken { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
