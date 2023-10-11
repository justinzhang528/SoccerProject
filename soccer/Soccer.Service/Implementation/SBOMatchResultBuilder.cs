using Soccer.Repository.Models;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultBuilder
    {
        IConfiguration _configuration;

        public SBOMatchResultBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<SBOMatchResultModel>> GenerateSBOMatchResultsAsync()
        {
            List<SBOMatchResultModel> sBOMatchResultModels = new List<SBOMatchResultModel>();
            // Create an HttpClient with a CookieContainer to manage cookies
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    // Create a CookieContainer and add the session cookie
                    var cookies = new CookieContainer();
                    var cookie = new Cookie(_configuration["SBOCookies:Name"], _configuration["SBOCookies:Value"]);
                    cookie.Domain = _configuration["SBOCookies:Domain"];
                    cookie.Path = _configuration["SBOCookies:Path"];
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
                    var response = await client.PostAsync(_configuration["URL:SBOSport-results-data"], postData);

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
                                    string homeTeam = list.ElementAt(2).ToString();
                                    string awayTeam = list.ElementAt(3).ToString();
                                    string matchTime = list.ElementAt(4).ToString();
                                    string isShowMoreData = list.ElementAt(7).ToString();
                                    string firstHalfScore = list.ElementAt(10).ToString();
                                    string fullTimeScore = list.ElementAt(11).ToString();
                                    SBOMatchResultModel model = new SBOMatchResultModel(id, league, homeTeam, awayTeam, matchTime, firstHalfScore, fullTimeScore, isShowMoreData);
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
                    // Create a CookieContainer and add the session cookie
                    var cookies = new CookieContainer();
                    var cookie = new Cookie(_configuration["SBOCookies:Name"], _configuration["SBOCookies:Value"]);
                    cookie.Domain = _configuration["SBOCookies:Domain"];
                    cookie.Path = _configuration["SBOCookies:Path"];
                    cookies.Add(cookie);
                    handler.CookieContainer = cookies;

                    // Define the payload data
                    FormUrlEncodedContent postData;
                    postData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("eventId", eventId)

                    });

                    // Send an HTTP POST request
                    var response = await client.PostAsync(_configuration["URL:SBOSport-results-more-data"], postData);

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
                                    string marketType = list.ElementAt(0).ToString();
                                    List<object> scores = JsonConvert.DeserializeObject<List<object>>(list.ElementAt(4).ToString());
                                    string firstHalfScore = scores.ElementAt(0).ToString();
                                    string fullTimeScore = scores.ElementAt(1).ToString();
                                    string code = list.ElementAt(5).ToString();

                                    SBOMatchDetailModel model = new SBOMatchDetailModel(eventId, marketType, firstHalfScore, fullTimeScore, code);
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
