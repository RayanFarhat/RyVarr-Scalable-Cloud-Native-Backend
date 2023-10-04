using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using BackendServer.Authentication;

namespace BackendServer.Hubs;

[Authorize(Roles = UserRoles.UserPro)]
public class HamzaCADHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", System.Net.Dns.GetHostName(), message + user);
    }
}