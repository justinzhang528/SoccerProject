using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Soccer.Models
{
    public class ResultBuilder
    {
        private string url;

        public ResultBuilder(string url)
        {
            this.url = url;
        }

        private void ParseHtml(string data, List<Result> results)
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

                    int status = 0; // 1->normal, 0->cancelled, 2->notStart

                    // 欄位
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
                        status = 1;
                        if (homeScore == "")
                            status = 2;
                    }

                    idx++;

                    // 如果是正常狀態，需要把detail資料取出來
                    Detail detail = null;
                    if (status == 1)
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

                        string[] teams = { teamsCells[0].InnerText, teamsCells[2].InnerText };
                        string[] firstHalf = { firstHalfCells[0].InnerText, firstHalfCells[2].InnerText };
                        string[] secondHalf = { secondHalfCells[0].InnerText, secondHalfCells[2].InnerText };
                        string[] regularTime = { regularTimeCells[0].InnerText, regularTimeCells[2].InnerText };
                        string[] corners = { cornersCells[0].InnerText, cornersCells[2].InnerText };
                        string[] penalties = { penaltiesCells[0].InnerText, penaltiesCells[2].InnerText };
                        string[] yellowCards = { yellowCardsCells[0].InnerText, yellowCardsCells[2].InnerText };
                        string[] redCards = { redCardsCells[0].InnerText, redCardsCells[2].InnerText };
                        string[] firstET = { firstETCells[0].InnerText, firstETCells[2].InnerText };
                        string[] secondET = { secondETCells[0].InnerText, secondETCells[2].InnerText };
                        string[] penaltiesShootout = { penaltiesShootoutCells[0].InnerText, penaltiesShootoutCells[2].InnerText };

                        idx += 12; //跳過detail表格裏面的11個rows，因爲已經取過了

                        detail = new Detail(teams, firstHalf, secondHalf, regularTime, corners, penalties, yellowCards, redCards, firstET, secondET, penaltiesShootout);
                    }

                    // 產生Result物件并且加到Result Array裏
                    string[] words = events.Split("vs");
                    string homeTeam = words[0];
                    string awayTeam = words[1];
                    Result result = new Result(gameTime, leagues, homeTeam, awayTeam, homeScore, awayScore, status, detail);
                    results.Add(result);
                }
            }
        }

        /// <summary>

        /// C#爬蟲獲取網頁中表格的數據

        /// </summary>

        private string GetHtmlContentFromNet(string url)
        {

            //爬取的網頁地址
            WebRequest request = WebRequest.Create(url);
            WebResponse response = (WebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            //此處將爬取到的內容轉換爲HTML

            string strHTML = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return strHTML;

        }

        public List<Result> GenerateResults()
        {
            string strHTML = GetHtmlContentFromNet(this.url);

            List<Result> results = new List<Result>();
            ParseHtml(strHTML, results);

            return results;
        }
    }
}
