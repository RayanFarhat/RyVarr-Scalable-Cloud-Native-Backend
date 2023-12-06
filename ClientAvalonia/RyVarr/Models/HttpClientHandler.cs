using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RyVarr.Models;

public class HttpClientHandler
{
    private string _apiBaseUrl { get; set; } = "http://localhost";
    public static string AuthToken { get; set; } = "";
    private HttpClient _client { get; set; } = new HttpClient();
    public HttpClientHandler()
    {
        // Set the base address for the HttpClient
        _client.BaseAddress = new Uri(_apiBaseUrl);
    }

    public async Task<HttpResponseMessage?> Req<ReqType>(string uri, string method, ReqType? Req = default(ReqType)) where ReqType : IReq
    {
        string jsonData = "";
        if (Req != null)
            jsonData = JsonSerializer.Serialize(Req);


        // Convert the JSON data to a StringContent object
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        try
        {
            // Add the Authorization header with the Bearer token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);

            // Send the request to the desired endpoint
            switch (method)
            {
                case "GET":
                    return await _client.GetAsync(uri);
                case "POST":
                    return await _client.PostAsync(uri, content);
                case "PUT":
                    return await _client.PutAsync(uri, content);
                case "DELETE":
                    return await _client.DeleteAsync(uri);
                default:
                    return null;
            }

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Error in the request or the response: " + e.Message);
            return null;
        }
    }
    public async Task<T?> Deserialize<T>(HttpResponseMessage response) where T : IRes
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(responseContent) ? default(T) : JsonSerializer.Deserialize<T>(responseContent)!;
    }
}

public record RegisterErrorsList(IReadOnlyList<string> Username, IReadOnlyList<string> Email, IReadOnlyList<string> Password);
public record LoginErrorsList(IReadOnlyList<string> Email, IReadOnlyList<string> Password);


public interface IReq { }
public record RegisterReq(string username, string email, string password) : IReq;
public record LoginReq(string email, string password) : IReq;

public interface IRes { }
public record RegisterRes400(string type, string title, int status, string traceId, RegisterErrorsList errors) : IRes;
public record RegisterRes200(string status, string message) : IRes;

public record LoginRes200(string token, DateTime expiration) : IRes;
public record LoginRes400(string type, string title, int status, string traceId, LoginErrorsList errors) : IRes;
public record LoginRes401(string type, string title, int status, string traceId) : IRes;

public record AccountRes200(string id, string username, bool isPro) : IRes;