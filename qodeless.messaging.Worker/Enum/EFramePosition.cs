namespace qodeless.messaging.Worker.Enum
{
    public enum EFramePosition : int
    {
        START_MESSAGE = 0,
        TOPIC = 1,
        BODY_SIZE = 2,/* 4 bytes*/
        START_BODY = 6/* Start Body*/
    }
}
