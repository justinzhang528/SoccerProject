using System.Net;
using System.Text;
using HtmlAgilityPack;
using Soccer.Repository.Interface;
using Soccer.Common.Utils;
using Soccer.Repository.Models;

namespace Soccer.Repository.Implementaion
{
    public class MatchResultBuilder : IMatchResultBuilder
    {
        private string _url;

        private void ParseHtml(string data, List<MatchResultModel> results)
        {
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                // 选择表格元素
                var table = doc.DocumentNode.SelectSingleNode("//table");
                if (table == null)
                    return;

                // 选择表格中的所有行
                var rows = table.SelectNodes(".//tr");
                if (rows == null)
                    return;

                int idx = 1;

                while (idx < rows.Count)
                {
                    var row = rows[idx];
                    var cells = row.SelectNodes(".//td");

                    EnumCondition condition = EnumCondition.Cancelled; // 1->normal, 0->cancelled, 2->notStart

                    // 欄位
                    string id = row.Attributes["id"].Value;
                    string gameTime = cells[0].InnerText;
                    string leagues = cells[1].InnerText;
                    string events = cells[2].InnerText;
                    string homeScore = "";
                    string awayScore = "";

                    // 正常或未開始狀態
                    if (cells.Count == 6)
                    {
                        homeScore = cells[3].InnerText;
                        awayScore = cells[4].InnerText;
                        condition = EnumCondition.Normal;
                        if (homeScore == "")
                            condition = EnumCondition.NotStart;
                    }

                    idx++;

                    // 如果是正常狀態，需要把detail資料取出來
                    MatchDetailModel detail = null;
                    if (condition == EnumCondition.Normal)
                    {
                        row = rows[idx];
                        var innerRows = row.SelectSingleNode(".//td").SelectSingleNode(".//table").SelectNodes(".//tr");
                        var teamsCells = innerRows[0].SelectNodes(".//td");
                        var firstHalfCells = innerRows[1].SelectNodes(".//td");
                        var secondHalfCells = innerRows[2].SelectNodes(".//td");
                        var regularTimeCells = innerRows[3].SelectNodes(".//td");
                        var cornersCells = innerRows[4].SelectNodes(".//td");
                        var penaltiesCells = innerRows[5].SelectNodes(".//td");
                        var yellowCardsCells = innerRows[6].SelectNodes(".//td");
                        var redCardsCells = innerRows[7].SelectNodes(".//td");
                        var firstETCells = innerRows[8].SelectNodes(".//td");
                        var secondETCells = innerRows[9].SelectNodes(".//td");
                        var penaltiesShootoutCells = innerRows[10].SelectNodes(".//td");

                        string firstHalf_H = firstHalfCells[0].InnerText;
                        string firstHalf_A = firstHalfCells[2].InnerText;
                        string secondHalf_H = secondHalfCells[0].InnerText;
                        string secondHalf_A = secondHalfCells[2].InnerText;
                        string regularTime_H = regularTimeCells[0].InnerText;
                        string regularTime_A = regularTimeCells[2].InnerText;
                        string corners_H = cornersCells[0].InnerText;
                        string corners_A = cornersCells[2].InnerText;
                        string penalties_H = penaltiesCells[0].InnerText;
                        string penalties_A = penaltiesCells[2].InnerText;
                        string yellowCards_H = yellowCardsCells[0].InnerText;
                        string yellowCards_A = yellowCardsCells[2].InnerText;
                        string redCards_H = redCardsCells[0].InnerText;
                        string redCards_A = redCardsCells[2].InnerText;
                        string firstET_H = firstETCells[0].InnerText;
                        string firstET_A = firstETCells[2].InnerText;
                        string secondET_H = secondETCells[0].InnerText;
                        string secondET_A = secondETCells[2].InnerText;
                        string penaltiesShootout_H = penaltiesShootoutCells[0].InnerText;
                        string penaltiesShootout_A = penaltiesShootoutCells[2].InnerText;

                        idx += 12; //跳過detail表格裏面的11個rows，因爲已經取過了

                        detail = new MatchDetailModel(id, firstHalf_H, firstHalf_A, secondHalf_H, secondHalf_A, regularTime_H, regularTime_A, corners_H, corners_A, penalties_H, penalties_A,
                            yellowCards_H, yellowCards_A, redCards_H, redCards_A, firstET_H, firstET_A, secondET_H, secondET_A, penaltiesShootout_H, penaltiesShootout_A);
                    }

                    // 產生Result物件并且加到Result Array裏
                    string[] words = events.Split("vs");
                    string homeTeam = words[0];
                    string awayTeam = words[1];
                    MatchResultModel result = new MatchResultModel(id, gameTime, leagues, homeTeam, awayTeam, homeScore, awayScore, condition, detail);
                    results.Add(result);
                }
            }
        }

        /// <summary>

        /// C#爬蟲獲取網頁中表格的數據

        /// </summary>

        private string GetHtmlContentFromNet()
        {

            //爬取的網頁地址
            WebRequest request = WebRequest.Create(_url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            //此處將爬取到的內容轉換爲HTML

            string strHTML = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return strHTML;

        }

        public List<MatchResultModel> GenerateResults()
        {
            string strHTML = GetHtmlContentFromNet();

            List<MatchResultModel> results = new List<MatchResultModel>();
            ParseHtml(strHTML, results);

            return results;
        }

        public void SetURL(string url)
        {
            _url = url;
        }
    }
}
