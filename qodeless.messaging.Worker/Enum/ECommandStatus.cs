namespace qodeless.messaging.Worker.Enum
{
    public enum ECommandStatus: byte
    {
        NAK = 0x01, //Not Acknoledge
        ACK = 0X02, //Acknoledge
        FAIL = 0X03,//Failed
        REFUSED = 0x55 //Denied
    }
}
