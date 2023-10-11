using Xunit.Abstractions;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using Soccer.Service.Implementation;
using Soccer.Repository.Models;

namespace SoccerTest
{
    public class SoccerTest
    {

        private readonly ITestOutputHelper output;

        public SoccerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task GenerateSBOResult()
        {
            SBOMatchResultBuilder sBOMatchResultBuilder = new SBOMatchResultBuilder();
            sBOMatchResultBuilder.SetUrl("https://sports-demo-wl.17mybet.com/web-root/restricted/result/results-data.aspx");
            List<SBOMatchResultModel> sBOMatchResultModels = await sBOMatchResultBuilder.GenerateSBOMatchResultsAsync();
            output.WriteLine(sBOMatchResultModels.Count.ToString());
            foreach (var item in sBOMatchResultModels)
            {
                output.WriteLine(item.Id);
                output.WriteLine(item.League);
                output.WriteLine(item.TeamA);
                output.WriteLine(item.TeamB);
                output.WriteLine(item.FirstHalfScore);
                output.WriteLine(item.FullTimeScore);
                output.WriteLine("------------------------------------");
            }
        }

        [Fact]
        public async Task GenerateSBOResultMoreData()
        {
            string eventId = "6012610";
            SBOMatchResultBuilder sBOMatchResultBuilder = new SBOMatchResultBuilder();
            sBOMatchResultBuilder.SetUrl("https://sports-demo-wl.17mybet.com/web-root/restricted/result/result-more-data.aspx");
            List<SBOMatchDetailModel> sBOMatchDetailModels = await sBOMatchResultBuilder.GenerateSBOMatchDetailsAsync(eventId);
            output.WriteLine(sBOMatchDetailModels.Count.ToString());
            foreach (var item in sBOMatchDetailModels)
            {
                output.WriteLine(item.Id);
                output.WriteLine(item.GoalType);
                output.WriteLine(item.FirstHalfScore);
                output.WriteLine(item.FullTimeScore);
                output.WriteLine(item.Code);
                output.WriteLine("------------------------------------");
            }

        }
    }
}