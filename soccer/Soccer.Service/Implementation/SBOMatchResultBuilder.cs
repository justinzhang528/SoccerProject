using Soccer.Repository.Models;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Soccer.Service.Interface;
using System.Text;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultBuilder : ISBOMatchResultBuilder
    {
        private readonly IConfiguration _configuration;
        private List<SBOMatchResultModel> _matchResults;
        private List<SBOMatchDetailModel> _matchDetails;
        private List<string> _eventIds;

        public SBOMatchResultBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            _matchResults = new List<SBOMatchResultModel>();
            _matchDetails = new List<SBOMatchDetailModel>();
            _eventIds = new List<string>();
        }

        private string GetPostRequestWithCookie(string url, string postData)
        {
            string res = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();

            var cookie = new Cookie(_configuration["SBOCookies:Name"], _configuration["SBOCookies:Value"])
            {
                Domain = _configuration["SBOCookies:Domain"],
                Path = _configuration["SBOCookies:Path"]
            };
            request.CookieContainer.Add(cookie);

            // Encode the POST data
            byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postDataBytes.Length;

            // Write the POST data to the request stream
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(postDataBytes, 0, postDataBytes.Length);
            }

            // Send the request and get the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Read the response content
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                res = reader.ReadToEnd();
            }

            response.Close();
            return res;
        }

        private string HandleStringNumber(string input)
        {
            string pattern = @"\d+";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                input = match.Value;
            }
            return input;
        }

        private void GenerateSBOMatchResults()
        {
            string url = _configuration["URL:SBOSport-results-data"];
            string postData = _configuration["PostData:ResultsData"];
            string content = GetPostRequestWithCookie(url, postData);

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
                            awayFirstHalfScore = HandleStringNumber(firstHalfScore[1].Trim());
                        }
                        if (fullTimeScore.Length == 2)
                        {
                            homeFullTimeScore = fullTimeScore[0].Trim();
                            awayFullTimeScore = HandleStringNumber(fullTimeScore[1].Trim());
                        }
                        SBOMatchResultModel model = new SBOMatchResultModel(id, league, homeTeam, awayTeam, matchTime, homeFirstHalfScore, awayFirstHalfScore, homeFullTimeScore, awayFullTimeScore, isShowMoreData);
                        _matchResults.Add(model);

                        if (isShowMoreData == "1")
                        {
                            _eventIds.Add(id);
                        }
                    }
                }
            }
        }

        private void GenerateSBOMatchDetails()
        {
            foreach (string eventId in _eventIds)
            {
                string url = _configuration["URL:SBOSport-results-more-data"];
                string postData = _configuration["PostData:ResultsMoreData"] + eventId;
                string content = GetPostRequestWithCookie(url, postData);

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
                            if (firstHalfScore.Length == 2)
                            {
                                homeFirstHalfScore = firstHalfScore[0].Trim();
                                awayFirstHalfScore = HandleStringNumber(firstHalfScore[1].Trim());
                            }
                            if (fullTimeScore.Length == 2)
                            {
                                homeFullTimeScore = fullTimeScore[0].Trim();
                                awayFullTimeScore = HandleStringNumber(fullTimeScore[1].Trim());
                            }
                            string code = list.ElementAt(5).ToString();

                            SBOMatchDetailModel model = new SBOMatchDetailModel(eventId, marketType, homeFirstHalfScore, awayFirstHalfScore, homeFullTimeScore, awayFullTimeScore, code);
                            _matchDetails.Add(model);
                        }
                    }
                }
            }
        }

        public void Build()
        {
            GenerateSBOMatchResults();
            GenerateSBOMatchDetails();
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
