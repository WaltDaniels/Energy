using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GridBankApi
{
    public class PushUpdate
    {
        private static readonly Lazy<PushUpdate> _instance =
            new Lazy<PushUpdate>(() => new PushUpdate(GlobalHost.ConnectionManager.GetHubContext<GridHub>().Clients));

        private PushUpdate(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static PushUpdate Instance
        {
            get { return _instance.Value; }
        }

        private static IHubConnectionContext<dynamic> Clients { get; set; }

        public static void BroadcastPowerLevel(decimal currentPower)
        {
            if (Clients != null)
            {
                Clients.All.broadCastMessage(string.Format("{0:P1}", currentPower));
            }
        }
    }
}