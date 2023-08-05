using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RyVarr.Models;

public class HttpClientHandler
{
    private string _apiBaseUrl { get; set; } = "http://localhost/";
    public string _authToken { get; set; } = "";
    private HttpClient _client { get; set; } = new HttpClient();
    public HttpClientHandler() {
        // Set the base address for the HttpClient
        _client.BaseAddress = new Uri(_apiBaseUrl);
    }
    public async Task<IRegisterRes?> RegisterReq(RegisterReq registerReq)
    {
        string jsonData = JsonSerializer.Serialize(registerReq);

        // Convert the JSON data to a StringContent object
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        try
        {
            // Send the POST request to the desired endpoint
            HttpResponseMessage response = await _client.PostAsync("register", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            // Handle the API response as needed
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<RegisterRes200>(responseContent)!;
            }
            else
            {
                return JsonSerializer.Deserialize<RegisterRes400>(responseContent)!;
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Error in the request or the response: " + e.Message);
            return null;
        }
    }

    public async Task<ILoginRes?> LoginReq(LoginReq loginReq)
    {
        string jsonData = JsonSerializer.Serialize(loginReq);

        // Convert the JSON data to a StringContent object
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        try
        {
            // Send the POST request to the desired endpoint
            HttpResponseMessage response = await _client.PostAsync("login", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            // Handle the API response as needed
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<LoginRes200>(responseContent)!;
            }
            else
            {
                return JsonSerializer.Deserialize<LoginRes401>(responseContent)!;
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Error in the request or the response: " + e.Message);
            return null;
        }
    }
}


public interface IRegisterRes { }
public record RegisterReq(string username, string email, string password);
public record RegisterErrorsList(IReadOnlyList<string> Email, IReadOnlyList<string> Password);
public record RegisterRes400(string type, string title, int status, string traceId, RegisterErrorsList errors): IRegisterRes;
public record RegisterRes200(string status,string message): IRegisterRes;

public interface ILoginRes { }

public record LoginReq(string email, string password);
public record LoginRes200(string token, DateTime expiration): ILoginRes;
public record LoginRes401(string type,string title, int status,string traceId): ILoginRes;