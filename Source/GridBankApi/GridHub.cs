using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GridBankApi
{
    [HubName("gridHub")]
    public class GridHub : Hub
    {
        private readonly PushUpdate _pushUpdate;

        public GridHub() : this(PushUpdate.Instance)
        {
        }

        public GridHub(PushUpdate pushUpdate)
        {
            _pushUpdate = pushUpdate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPower"></param>
        public void UsageUpdate(decimal currentPower)
        {
            Clients.All.broadCastMessage(currentPower.ToString());
        }
    }
}