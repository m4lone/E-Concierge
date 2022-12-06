using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Hubs
{
    //SignarlR SERVER
    public class QodelessHub : Hub
    {
        public class Message
        {
            public string UserName { get; set; }
            public string Text { get; set; }
        }
    }
}
