using HamzaCad.BarsComputation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HamzaCad.Utils
{
    public class HttpClientHandler
    {
#if DEBUG
        private string _apiBaseUrl { get; set; } = "http://localhost";
#else
        private string _apiBaseUrl { get; set; } = "https://ryvarr.com";
#endif
        public static string AuthToken { get; set; } = "";
        private HttpClient _client { get; set; } = new HttpClient();
        public HttpClientHandler()
        {
            // Set the base address for the HttpClient
            _client.BaseAddress = new Uri(_apiBaseUrl);

        }

        public async Task<double> Req(Angle a)
        {
            string jsonData = "";
            jsonData = JsonSerializer.Serialize(a);


            // Convert the JSON data to a StringContent object
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                // Add the Authorization header with the Bearer token
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
                HttpResponseMessage res = await _client.PostAsync("/api/HamzaCAD/angle", content);
                if (res.IsSuccessStatusCode)
                {
                    string responseContent = await res.Content.ReadAsStringAsync();
                    double angle;
                    if (Double.TryParse(responseContent, out angle))
                    {
                        return angle;
                    }
                    else
                    {
                        // 1000 then false or failed
                        return 1000;
                    }
                }
                else
                {
                    BarsComputer.ed.WriteMessage("UnAuthorized\n");
                    return 1000;
                }
            }
            catch (HttpRequestException e)
            {
                BarsComputer.ed.WriteMessage("Error in the request or the response: " + e.Message);
                return 1000;
            }
        }
    }
    public class Angle
    {
        public double p1x { get; set; }
        public double p1y { get; set; }
        public double p2x { get; set; }
        public double p2y { get; set; }
        public Angle(double p1x, double p1y, double p2x, double p2y)
        {
            this.p1x = p1x;
            this.p1y = p1y;
            this.p2x = p2x;
            this.p2y = p2y;
        }
    }
}
