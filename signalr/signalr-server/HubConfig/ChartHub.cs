using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace signalr_server.HubConfig
{
    public class ChartHub: Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public async Task SendPrivateMessage(string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("transferchartdata", DataManager.GetData());
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage()
        {
            await Clients.All.SendAsync("transferchartdata", DataManager.GetData());
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}