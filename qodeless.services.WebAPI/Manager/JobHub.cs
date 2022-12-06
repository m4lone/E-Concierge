using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Manager
{
    public class JobHub 
    {
        private static HubConnection connection;
        public static HubConnectionState InitQodelessHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44302/QodelessHub")
                .Build();
            connection.StartAsync();

            return connection.State;
        }
        public static HubConnectionState StopQodelessHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44302/QodelessHub")
                .Build();
            connection.StopAsync();

            return connection.State;
        }
    }
}
