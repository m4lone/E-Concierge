using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class ChargeRequest
    {
        public ChargeRequest(string userPlayId)
        {
            UserPlayId = userPlayId;
            //  Amount = amount;    
        }

        //   public Guid Id { get; set; }
        public string UserPlayId { get; set; }
        //   public string UserOperationId { get; set; }
        //   public EBalanceType Type { get; set; }
        //   public double Amount { get; set; }
        //   public string Description { get; set; }
        //   public double TotalBalances { get; set; } 
    }
}
