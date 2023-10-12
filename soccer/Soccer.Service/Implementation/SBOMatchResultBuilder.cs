    using Soccer.Repository.Models;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Soccer.Service.Interface;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultBuilder : ISBOMatchResultBuilder
    {
        private readonly IConfiguration _configuration;
        private List<SBOMatchResultModel> _matchResults;
        private List<SBOMatchDetailModel> _matchDetails;

        public SBOMatchResultBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            _matchResults = new List<SBOMatchResultModel>();
            _matchDetails = new List<SBOMatchDetailModel>();
        }
        private async Task<string> SendPostRequestWithCookieAsync(string url, FormUrlEncodedContent postData)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    var cookies = new CookieContainer();
                    var cookie = new Cookie(_configuration["SBOCookies:Name"], _configuration["SBOCookies:Value"])
                    {
                        Domain = _configuration["SBOCookies:Domain"],
                        Path = _configuration["SBOCookies:Path"]
                    };
                    cookies.Add(cookie);
                    handler.CookieContainer = cookies;

                    var response = await client.PostAsync(url, postData);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return null; // Handle the case when the request fails
        }

        private async Task GenerateSBOMatchResultsAsync()
        {
            List<SBOMatchResultModel> sBOMatchResultModels = new List<SBOMatchResultModel>();

            FormUrlEncodedContent postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("result-token", "1,1,1"),
                new KeyValuePair<string, string>("actionValue", "today"),
                new KeyValuePair<string, string>("leagueid", ""),
                new KeyValuePair<string, string>("startdate", ""),
                new KeyValuePair<string, string>("names", ""),
                new KeyValuePair<string, string>("isMobile", "0"),
                new KeyValuePair<string, string>("HidCK", "uJdZa")
            });

            string content = await SendPostRequestWithCookieAsync(_configuration["URL:SBOSport-results-data"], postData);

            if (!string.IsNullOrEmpty(content))
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
                        string homeFirstHalfScore = "-1";
                        string awayFirstHalfScore = "-1";
                        string homeFullTimeScore = "-1";
                        string awayFullTimeScore = "-1";
                        string[] firstHalfScore = list.ElementAt(10).ToString().Split(":");
                        string[] fullTimeScore = list.ElementAt(11).ToString().Split(":");
                        if (firstHalfScore.Length == 2)
                        {
                            homeFirstHalfScore = firstHalfScore[0].Trim();
                            awayFirstHalfScore = firstHalfScore[1].Trim();
                        }
                        if (fullTimeScore.Length == 2)
                        {
                            homeFullTimeScore = fullTimeScore[0].Trim();
                            awayFullTimeScore = fullTimeScore[1].Trim();
                        }
                        SBOMatchResultModel model = new SBOMatchResultModel(id, league, homeTeam, awayTeam, matchTime, homeFirstHalfScore, awayFirstHalfScore, homeFullTimeScore, awayFullTimeScore, isShowMoreData);
                        _matchResults.Add(model);
                        if(isShowMoreData == "1")
                        {
                            await GenerateSBOMatchDetailsAsync(id);
                        }
                    }
                }
            }
        }

        private async Task GenerateSBOMatchDetailsAsync(string eventId)
        {
            List<SBOMatchDetailModel> sBOMatchDetailModels = new List<SBOMatchDetailModel>();

            FormUrlEncodedContent postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("eventId", eventId)
            });

            string content = await SendPostRequestWithCookieAsync(_configuration["URL:SBOSport-results-more-data"], postData);

            if (!string.IsNullOrEmpty(content))
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
                        string homeFirstHalfScore = "-1";
                        string awayFirstHalfScore = "-1";
                        string homeFullTimeScore = "-1";
                        string awayFullTimeScore = "-1";
                        string[] firstHalfScore = scores.ElementAt(0).ToString().Split(":");
                        string[] fullTimeScore = scores.ElementAt(1).ToString().Split(":");
                        if(firstHalfScore.Length == 2)
                        {
                            homeFirstHalfScore = firstHalfScore[0].Trim();
                            awayFirstHalfScore = firstHalfScore[1].Trim();
                        }
                        if(fullTimeScore.Length == 2)
                        {
                            homeFullTimeScore = fullTimeScore[0].Trim();
                            awayFullTimeScore = fullTimeScore[1].Trim();
                        }
                        string code = list.ElementAt(5).ToString();

                        SBOMatchDetailModel model = new SBOMatchDetailModel(eventId, marketType, homeFirstHalfScore, awayFirstHalfScore, homeFullTimeScore, awayFullTimeScore, code);
                        _matchDetails.Add(model);
                    }
                }
            }
        }

        public async Task Build()
        {
            await GenerateSBOMatchResultsAsync();
        }

        public List<SBOMatchResultModel> GetSBOMatchResults()
        {
            return _matchResults;
        }

        public List<SBOMatchDetailModel> GetSBOMatchDetails()
        {
            return _matchDetails;
        }

    }
}
