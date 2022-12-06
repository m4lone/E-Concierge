using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class PaymentResponse : BaseResponse
    {
        public object Base64 { get; set; } //QRCODE DO PIX
        public string QrCode { get; set; } //COPIA E COLA PIX
        public string PixId { get; set; }
    }
}
