using Microsoft.AspNetCore.Mvc;
using Orleans.Runtime;
using System.Net;

namespace BackendServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    private readonly IClusterClient _clusterClient;

    public HelloController(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    [HttpGet("{message}/{id}")]
    public async Task<IActionResult> Get(string message, int id)
    {
        System.Console.WriteLine("hi");
        var dns = Dns.GetHostName();
        var helloGrain = _clusterClient.GetGrain<IHelloGrain>(id); // Use appropriate grain key
        var state = await helloGrain.SetUrl(message);
        return Ok(new Res(await helloGrain.GetUrl(), state, dns));
    }
}
public interface IHelloGrain : IGrainWithIntegerKey
{
    Task<string> SetUrl(string fullUrl);
    Task<string> GetUrl();
}

public class HelloGrain : Grain, IHelloGrain
{
    private readonly IPersistentState<UrlDetails> _state;

    public HelloGrain(
        [PersistentState(
            stateName: "url",
            storageName: "urls")]
            IPersistentState<UrlDetails> state) => _state = state;

    public async Task<string> SetUrl(string fullUrl)
    {
        _state.State = new()
        {
            FullUrl = fullUrl
        };

        await _state.WriteStateAsync();

        return Dns.GetHostName();
    }

    public Task<string> GetUrl() =>
        Task.FromResult(_state.State.FullUrl);
}

[Serializable]
public record class UrlDetails
{
    [Id(0)]
    public string FullUrl { get; set; } = "";
}

public record class Res(string stateFullUrl, string stateDns, string Dns);
