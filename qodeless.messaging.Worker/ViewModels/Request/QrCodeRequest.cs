namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class QrCodeRequest
    {
        public QrCodeRequest(string code, string playerId)
        {
            Code = code;
            PlayerId = playerId;
        }

        public string Code { get; set; }
        public string PlayerId { get; set; }
    }
}
