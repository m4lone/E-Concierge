using qodeless.messaging.Worker.ViewModels.Common;
using System;
using System.Collections.Generic;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class GameCoinBetResponse : BaseResponse
    {
        public List<int> GameCoinBet { get; set; }
    }
}
