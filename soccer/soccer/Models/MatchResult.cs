using Soccer.Utils;

namespace Soccer.Models
{
    public class MatchResult
    {
        public string Id { get; set; }
        public string GameTime { get; set; }
        public string Leagues { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public MatchDetail Detail { get; set; }
        public ConditionInfo Condition { get; set; }

        public MatchResult() { }

        public MatchResult(string id, string gameTime, string leagues, string homeTeam, string awayTeam, string homeScore, string awayScore, ConditionInfo condition, MatchDetail detail)
        {
            Id = id;
            GameTime = gameTime;
            Leagues = leagues;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Condition = condition;
            Detail = detail;
        }

        public void PrintInfo()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine(Id);
            Console.WriteLine(GameTime);
            Console.WriteLine(Leagues);
            Console.WriteLine(HomeTeam + " vs " + AwayTeam);
            if (Condition == ConditionInfo.Normal)
            {
                Console.WriteLine(HomeScore);
                Console.WriteLine(AwayScore);
                Console.WriteLine();
                Console.WriteLine("Detail:");
                Console.WriteLine(HomeTeam + " teams " + AwayTeam);
                Console.WriteLine(Detail.FirstHalf_H + " firstHalf " + Detail.FirstHalf_A);
                Console.WriteLine(Detail.SecondHalf_H + " secondHalf " + Detail.SecondHalf_A);
                Console.WriteLine(Detail.RegularTime_H + " regularTime " + Detail.RegularTime_A);
                Console.WriteLine(Detail.Corners_H + " corners " + Detail.Corners_A);
                Console.WriteLine(Detail.Penalties_H + " penalties " + Detail.Penalties_A);
                Console.WriteLine(Detail.YellowCards_H + " yellowCards " + Detail.YellowCards_A);
                Console.WriteLine(Detail.RedCards_H + " redCards " + Detail.RedCards_A);
                Console.WriteLine(Detail.FirstHalf_H + " firstET " + Detail.FirstHalf_A);
                Console.WriteLine(Detail.SecondHalf_H + " secondET " + Detail.SecondHalf_A);
                Console.WriteLine(Detail.PenaltiesShootout_H + " penaltiesShootout " + Detail.PenaltiesShootout_A);
            }
            else if (Condition == ConditionInfo.Cancelled)
            {
                Console.WriteLine();
                Console.WriteLine("Cancelled");
            }
            Console.WriteLine("------------------------------------");
        }
    }
}
