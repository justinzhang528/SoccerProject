﻿using Soccer.Repository.Models;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Soccer.Service.Interface;
using System.Text;
using HtmlAgilityPack;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultBuilder : ISBOMatchResultBuilder
    {
        private readonly IConfiguration _configuration;
        private List<SBOMatchResultModel> _matchResults;
        private List<SBOMatchDetailModel> _matchDetails;
        private List<string> _eventIds;
        private Dictionary<string, string> _cookies;

        public SBOMatchResultBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            _matchResults = new List<SBOMatchResultModel>();
            _matchDetails = new List<SBOMatchDetailModel>();
            _eventIds = new List<string>();
            _cookies = GetCookiesFromNet();
            _cookies["Value"] = _configuration["SBOCookies:Value"];
        }

        private string GetResponseWithCookie(string url, string method, string? postData = null)
        {
            string res = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();

            var cookie = new Cookie(_cookies["Name"], _cookies["Value"])
            {
                Domain = _cookies["Domain"],
                Path = _cookies["Path"]
            };
            request.CookieContainer.Add(cookie);

            if (postData != null)
            {
                // Encode the POST data
                byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = postDataBytes.Length;

                // Write the POST data to the request stream
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
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
            string postData = _configuration["PostData:ResultsData"] + GetHidCK();
            string content = GetResponseWithCookie(url, "POST", postData);

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
                string content = GetResponseWithCookie(url, "POST", postData);

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

        private Dictionary<string,string> GetCookiesFromNet()
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            string url = _configuration["URL:SBOSport-cookies"];
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                // Print the properties of each cookie.
                foreach (Cookie cook in response.Cookies)
                {
                    cookies["Name"] = cook.Name;
                    cookies["Value"] = cook.Value;
                    cookies["Domain"] = cook.Domain;
                    cookies["Path"] = cook.Path;
                }
            }

            return cookies;
        }

        private string GetHidCK()
        {
            string url = _configuration["URL:SBOSport-results-more"];
            string htmlStr = GetResponseWithCookie(url, "GET");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);
            string hidCK = doc.DocumentNode.SelectSingleNode("//input[@name='HidCK']").GetAttributeValue("value", "");
            return hidCK;
        }
    }
}