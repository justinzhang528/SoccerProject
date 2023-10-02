using AngleSharp.Dom;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using HtmlAgilityPack;

namespace ConsoleApplication
{
    class Program
    {
        public static void ConvertToData(string data)
        {
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                // 选择表格元素
                var table = doc.DocumentNode.SelectSingleNode("//table");

                if (table != null)
                {
                    // 选择表格中的所有行
                    var rows = table.SelectNodes(".//tr");

                    if (rows != null)
                    {
                        // 遍历每一行并提取数据
                        foreach (var row in rows) // 跳过第一行（表头）
                        {
                            var cells = row.SelectNodes(".//td");
                            foreach (var cell in cells)
                            {
                                Console.WriteLine(cell.InnerText);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>

        /// C#爬蟲獲取網頁中表格的數據

        /// </summary>

        public static void GetDataFromNet()
        {

            //爬取的網頁地址
            string url = "https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8";

            WebRequest request = WebRequest.Create(url);

            WebResponse response = (WebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            //此處將爬取到的內容轉換爲HTML

            string strHTML = reader.ReadToEnd();

            //Console.WriteLine(strHTML);

            ConvertToData(strHTML);

            File.WriteAllText("output.txt", strHTML);

            reader.Close();

            dataStream.Close();

            response.Close();

        }

        static void Main(string[] args)
        {
            GetDataFromNet();
        }
    }
}