namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class TokenRequest
    {
        public TokenRequest(string phone)
        {
            Phone = phone;
        }

        public string Phone { get; set; }
    }
}
