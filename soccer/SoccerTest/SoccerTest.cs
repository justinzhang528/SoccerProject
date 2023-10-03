using Soccer.Models;
using System;
using Xunit.Abstractions;

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
        public void GenerateResultsTest()
        {
            string url = "https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8";
            ResultBuilder builder = new ResultBuilder(url);
            List<Result> results = builder.GenerateResults();

            foreach (Result result in results) 
            {
                output.WriteLine("------------------------------------");
                output.WriteLine(result.GameTime);
                output.WriteLine(result.Leagues);
                output.WriteLine(result.HomeTeam + " vs " + result.AwayTeam);
                if (result.Status == 1)
                {
                    output.WriteLine(result.HomeScore);
                    output.WriteLine(result.AwayScore);
                    output.WriteLine("");
                    output.WriteLine("Detail:");
                    output.WriteLine(result.Detail.Teams[0] + " teams " + result.Detail.Teams[1]);
                    output.WriteLine(result.Detail.FirstHalf[0] + " firstHalf " + result.Detail.FirstHalf[1]);
                    output.WriteLine(result.Detail.SecondHalf[0] + " secondHalf " + result.Detail.SecondHalf[1]);
                    output.WriteLine(result.Detail.RegularTime[0] + " regularTime " + result.Detail.RegularTime[1]);
                    output.WriteLine(result.Detail.Corners[0] + " corners " + result.Detail.Corners[1]);
                    output.WriteLine(result.Detail.Penalties[0] + " penalties " + result.Detail.Penalties[1]);
                    output.WriteLine(result.Detail.YellowCards[0] + " yellowCards " + result.Detail.YellowCards[1]);
                    output.WriteLine(result.Detail.RedCards[0] + " redCards " + result.Detail.RedCards[1]);
                    output.WriteLine(result.Detail.FirstHalf[0] + " firstET " + result.Detail.FirstHalf[1]);
                    output.WriteLine(result.Detail.SecondHalf[0] + " secondET " + result.Detail.SecondHalf[1]);
                    output.WriteLine(result.Detail.PenaltiesShootout[0] + " penaltiesShootout " + result.Detail.PenaltiesShootout[1]);
                }
                else if (result.Status == 0)
                {
                    output.WriteLine("");
                    output.WriteLine("Cancelled");
                }
                output.WriteLine("------------------------------------");
            }
        }
    }
}