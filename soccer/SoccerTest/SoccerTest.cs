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
                output.WriteLine(result.Id);
                output.WriteLine(result.GameTime);
                output.WriteLine(result.Leagues);
                output.WriteLine(result.HomeTeam + " vs " + result.AwayTeam);
                if (result.Status == 1)
                {
                    output.WriteLine(result.HomeScore);
                    output.WriteLine(result.AwayScore);
                    output.WriteLine("");
                    output.WriteLine("Detail:");
                    output.WriteLine(result.Detail.Id);
                    output.WriteLine(result.HomeTeam + " teams " + result.AwayTeam);
                    output.WriteLine(result.Detail.FirstHalf_H + " firstHalf " + result.Detail.FirstHalf_A);
                    output.WriteLine(result.Detail.SecondHalf_H + " secondHalf " + result.Detail.SecondHalf_A);
                    output.WriteLine(result.Detail.RegularTime_H + " regularTime " + result.Detail.RegularTime_A);
                    output.WriteLine(result.Detail.Corners_H + " corners " + result.Detail.Corners_A);
                    output.WriteLine(result.Detail.Penalties_H + " penalties " + result.Detail.Penalties_A);
                    output.WriteLine(result.Detail.YellowCards_H + " yellowCards " + result.Detail.YellowCards_A);
                    output.WriteLine(result.Detail.RedCards_H + " redCards " + result.Detail.RedCards_A);
                    output.WriteLine(result.Detail.FirstHalf_H + " firstET " + result.Detail.FirstHalf_A);
                    output.WriteLine(result.Detail.SecondHalf_H + " secondET " + result.Detail.SecondHalf_A);
                    output.WriteLine(result.Detail.PenaltiesShootout_H + " penaltiesShootout " + result.Detail.PenaltiesShootout_A);
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