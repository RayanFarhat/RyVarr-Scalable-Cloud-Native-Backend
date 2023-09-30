using Microsoft.AspNetCore.SignalR;

namespace BackendServer.Hubs;

public class HamzaCADHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", System.Net.Dns.GetHostName(), message);
    }
}