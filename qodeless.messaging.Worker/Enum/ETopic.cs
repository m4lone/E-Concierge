namespace qodeless.messaging.Worker.Enum
{
    public enum ETopic : byte
    {
        Token = 0x50,
        Join,       //After SMS Token to AspNetUsers
        Subscribe,  //QrCode to Site
        Login,
        Logout,
        DeviceIn,
        DeviceOut,
        DeviceStatus,   //Se o device está busy
        Play,
        QrCode,
        Charge, //Virtual Balance - Assim que o Unity entrar no jogo verifica se o UserPlayId tem algum crédito (amount) para jogar
        Payment,
        PixPaidConfirmation,
        Register, //Pré Registro do usúario pelo unity 
        CompleteRegister,
        GetUser,
        Voucher,
        CreditPlayer,
        GameCoinBet
    }
}
