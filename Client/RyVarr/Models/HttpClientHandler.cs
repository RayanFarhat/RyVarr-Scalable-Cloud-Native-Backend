using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RyVarr.Models;

public class HttpClientHandler
{
    private string _apiBaseUrl { get; set; } = "http://localhost/api";
    public static string AuthToken { get; set; } = "";
    private HttpClient _client { get; set; } = new HttpClient();
    public HttpClientHandler() {
        // Set the base address for the HttpClient
        _client.BaseAddress = new Uri(_apiBaseUrl);
    }

    public async Task<IRes?> Req<T>(string uri, string method, IReq? Req = null) where T : IRes
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
            HttpResponseMessage response;
            switch (method)
            {
                case "GET":
                    response = await _client.GetAsync(uri);
                    break;
                case "POST":
                    response = await _client.PostAsync(uri, content);
                    break;
                case "PUT":
                    response = await _client.PutAsync(uri, content);
                    break;
                case "DELETE":
                    response = await _client.DeleteAsync(uri);
                    break;
                default:
                    return default(T);
            }
            string responseContent = await response.Content.ReadAsStringAsync();
            // Handle the API response as needed

            return JsonSerializer.Deserialize<T>(responseContent);
     
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Error in the request or the response: " + e.Message);
            return default(T);
        }
    }
}
public record RegisterErrorsList(IReadOnlyList<string> Email, IReadOnlyList<string> Password);

public interface IReq { }
public record RegisterReq(string username, string email, string password):IReq;
public record LoginReq(string email, string password) : IReq;

public interface IRes { }
public record RegisterRes400(string type, string title, int status, string traceId, RegisterErrorsList errors): IRes;
public record RegisterRes200(string status,string message): IRes;

public record LoginRes200(string token, DateTime expiration): IRes;
public record LoginRes401(string type,string title, int status,string traceId): IRes;

public record AccountRes200(string status, string message) : IRes;