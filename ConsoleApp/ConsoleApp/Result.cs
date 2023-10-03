using AngleSharp.Dom.Events;
using System;

namespace ConsoleApp
{
    internal class Result
    {
        public string GameTime { get; set; }
        public string Leagues { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public Detail Detail { get; set; }
        public int Status { get; set; } // 1->normal, 0->cancelled, 2->notStart

        public Result(string gameTime, string leagues, string homeTeam, string awayTeam, string homeScore, string awayScore, int status, Detail detail) 
        {
            GameTime = gameTime;
            Leagues = leagues;                
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Status = status;
            Detail = detail;
        }

        public void PrintInfo()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine(GameTime);
            Console.WriteLine(Leagues);
            Console.WriteLine(HomeTeam + " vs " + AwayTeam);
            if (Status == 1) 
            {
                Console.WriteLine(HomeScore);
                Console.WriteLine(AwayScore);
                Console.WriteLine();
                Console.WriteLine("Detail:");
                Console.WriteLine(Detail.Teams[0] + " teams " + Detail.Teams[1]);
                Console.WriteLine(Detail.FirstHalf[0] + " firstHalf " + Detail.FirstHalf[1]);
                Console.WriteLine(Detail.SecondHalf[0] + " secondHalf " + Detail.SecondHalf[1]);
                Console.WriteLine(Detail.RegularTime[0] + " regularTime " + Detail.RegularTime[1]);
                Console.WriteLine(Detail.Corners[0] + " corners " + Detail.Corners[1]);
                Console.WriteLine(Detail.Penalties[0] + " penalties " + Detail.Penalties[1]);
                Console.WriteLine(Detail.YellowCards[0] + " yellowCards " + Detail.YellowCards[1]);
                Console.WriteLine(Detail.RedCards[0] + " redCards " + Detail.RedCards[1]);
                Console.WriteLine(Detail.FirstHalf[0] + " firstET " + Detail.FirstHalf[1]);
                Console.WriteLine(Detail.SecondHalf[0] + " secondET " + Detail.SecondHalf[1]);
                Console.WriteLine(Detail.PenaltiesShootout[0] + " penaltiesShootout " + Detail.PenaltiesShootout[1]);
            }
            else if(Status == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Cancelled");
            }
            Console.WriteLine("------------------------------------");
        }
    }
}
