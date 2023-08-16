using System.Net;
using System.Net.Sockets;
using Orleans.Configuration;

namespace BackendServer.Startups;
public class OrleansStartup
{
    public static async Task Init(WebApplicationBuilder builder)
    {
        var hostName = Dns.GetHostName();
        var hostEntry = await Dns.GetHostEntryAsync(hostName);
        var ip = hostEntry.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
        builder.Host.UseOrleans((_, silo) => silo
        .UseRedisClustering("redis:6379")
        .AddMemoryGrainStorageAsDefault()
        .Configure<EndpointOptions>(options =>
        {
            options.AdvertisedIPAddress = ip;
        })
        );
    }
}