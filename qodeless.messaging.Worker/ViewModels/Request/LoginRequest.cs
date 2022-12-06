namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class LoginRequest
    {
        public LoginRequest(string phone, string password)
        {
            Phone = phone;
            Password = password;
        }

        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
