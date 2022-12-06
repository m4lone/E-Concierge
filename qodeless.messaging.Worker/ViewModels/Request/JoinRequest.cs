namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class JoinRequest
    {
        public JoinRequest(string phone, string inviteToken)
        {
            Phone = phone;
            InviteToken = inviteToken;
        }

        public string Phone { get; set; }
        public string InviteToken { get; set; }
    }
}
