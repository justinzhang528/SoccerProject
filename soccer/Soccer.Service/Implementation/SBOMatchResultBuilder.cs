using Soccer.Repository.Models;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultBuilder
    {
        private string _url;

        public void SetUrl(string url)
        {
            _url = url;
        }

        public async Task<List<SBOMatchResultModel>> GenerateSBOMatchResultsAsync()
        {
            List<SBOMatchResultModel> sBOMatchResultModels = new List<SBOMatchResultModel>();
            // Create an HttpClient with a CookieContainer to manage cookies
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    // Set the URL of the website you want to crawl
                    // Create a CookieContainer and add the session cookie
                    var cookies = new CookieContainer();
                    var cookie = new Cookie("ASP.NET_SessionId", "rqlbxric0qwgm4m1nlxjiowx");
                    cookie.Domain = "sports-demo-wl.17mybet.com";
                    cookie.Path = "/";
                    cookies.Add(cookie);
                    handler.CookieContainer = cookies;

                    // Define the payload data
                    FormUrlEncodedContent postData;
                    postData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("result-token", "1,1,1"),
                        new KeyValuePair<string, string>("actionValue", "today"),
                        new KeyValuePair<string, string>("leagueid", ""),
                        new KeyValuePair<string, string>("startdate", ""),
                        new KeyValuePair<string, string>("names", ""),
                        new KeyValuePair<string, string>("isMobile", "0"),
                        new KeyValuePair<string, string>("HidCK", "uJdZa")

                    });

                    // Send an HTTP POST request
                    var response = await client.PostAsync(_url, postData);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string content = await response.Content.ReadAsStringAsync();

                        if (content != null)
                        {
                            string pattern = @"<script>(.*?)</script>";
                            MatchCollection matches = Regex.Matches(content, pattern);

                            if (matches.Count > 0)
                            {
                                string resultString = matches[matches.Count - 1].Groups[1].Value;
                                resultString = Regex.Match(resultString, @"\[.*\]").Value;
                                resultString = resultString.Replace("\\x", "");
                                List<object>? listResult = JsonConvert.DeserializeObject<List<object>>(resultString);
                                foreach (var result in listResult)
                                {
                                    List<object> list = JsonConvert.DeserializeObject<List<object>>(result.ToString());
                                    string id = list.ElementAt(0).ToString();
                                    string league = list.ElementAt(1).ToString();
                                    string teamA = list.ElementAt(2).ToString();
                                    string teamB = list.ElementAt(3).ToString();
                                    string dateTime = list.ElementAt(4).ToString();
                                    string isShowMore = list.ElementAt(7).ToString();
                                    string firstHalfScore = list.ElementAt(10).ToString();
                                    string fullTimeScore = list.ElementAt(11).ToString();
                                    SBOMatchResultModel model = new SBOMatchResultModel(id, league, teamA, teamB, dateTime, firstHalfScore, fullTimeScore, isShowMore);
                                    sBOMatchResultModels.Add(model);
                                }
                            }
                        }

                    }
                }
            }
            return sBOMatchResultModels;
        }

        public async Task<List<SBOMatchDetailModel>> GenerateSBOMatchDetailsAsync(string eventId)
        {
            List<SBOMatchDetailModel> sBOMatchDetailModels = new List<SBOMatchDetailModel>();
            // Create an HttpClient with a CookieContainer to manage cookies
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    // Set the URL of the website you want to crawl
                    // Create a CookieContainer and add the session cookie
                    var cookies = new CookieContainer();
                    var cookie = new Cookie("ASP.NET_SessionId", "rqlbxric0qwgm4m1nlxjiowx");
                    cookie.Domain = "sports-demo-wl.17mybet.com";
                    cookie.Path = "/";
                    cookies.Add(cookie);
                    handler.CookieContainer = cookies;

                    // Define the payload data
                    FormUrlEncodedContent postData;
                    postData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("eventId", eventId)

                    });

                    // Send an HTTP POST request
                    var response = await client.PostAsync(_url, postData);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string content = await response.Content.ReadAsStringAsync();

                        if (content != null)
                        {
                            string pattern = @"<script>(.*?)</script>";
                            MatchCollection matches = Regex.Matches(content, pattern);

                            if (matches.Count > 0)
                            {
                                string resultString = matches[matches.Count - 1].Groups[1].Value;
                                resultString = Regex.Match(resultString, @"\[.*\]").Value;

                                List<object>? listDetail = JsonConvert.DeserializeObject<List<object>>(resultString);

                                listDetail = JsonConvert.DeserializeObject<List<object>>(listDetail.ElementAt(4).ToString());

                                foreach (var item in listDetail)
                                {
                                    List<object> list = JsonConvert.DeserializeObject<List<object>>(item.ToString());
                                    string goalType = list.ElementAt(0).ToString();
                                    string firstHalfScore = list.ElementAt(4).ToString();
                                    string fullTimeScore = list.ElementAt(4).ToString();
                                    string code = list.ElementAt(5).ToString();

                                    SBOMatchDetailModel model = new SBOMatchDetailModel(eventId, goalType, firstHalfScore, fullTimeScore, code);
                                    sBOMatchDetailModels.Add(model);
                                }
                            }
                        }

                    }
                }
            }
            return sBOMatchDetailModels;
        }

    }
}
