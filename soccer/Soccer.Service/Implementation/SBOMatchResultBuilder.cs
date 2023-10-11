using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultBuilder
    {
        private async Task<string> GetCrawlingContentFromNetAsync()
        {
            string res = "";
            // Create an HttpClient with a CookieContainer to manage cookies
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    // Set the URL of the website you want to crawl
                    string url = "https://sports-demo-wl.17mybet.com/web-root/restricted/result/results-data.aspx";
                    // Create a CookieContainer and add the session cookie
                    var cookies = new CookieContainer();
                    var cookie = new Cookie("ASP.NET_SessionId", "rqlbxric0qwgm4m1nlxjiowx");
                    cookie.Domain = "sports-demo-wl.17mybet.com";
                    cookie.Path = "/";
                    cookies.Add(cookie);
                    handler.CookieContainer = cookies;

                    // Define the payload data
                    var postData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("result-token", "1,1,1"),
                        new KeyValuePair<string, string>("actionValue", "today"),
                        new KeyValuePair<string, string>("leagueid", ""),
                        new KeyValuePair<string, string>("startdate", ""),
                        new KeyValuePair<string, string>("names", ""),
                        new KeyValuePair<string, string>("isMobile", "0"),
                        new KeyValuePair<string, string>("HidCK", "uJdZa")
                    });

                    // Send an HTTP GET request
                    var response = await client.PostAsync(url, postData);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                       res = await response.Content.ReadAsStringAsync();
                        
                    }
                }
            }

            return res;
        }

        public Task<string> GetString()
        {
            var res = GetCrawlingContentFromNetAsync();
            return res;
        }
    }
}
