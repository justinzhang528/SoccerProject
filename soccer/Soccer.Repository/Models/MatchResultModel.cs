using Soccer.Common.Utils;

namespace Soccer.Models
{
    public class MatchResultModel
    {
        public string Id { get; set; }
        public string GameTime { get; set; }
        public string Leagues { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public MatchDetailModel Detail { get; set; }
        public EnumCondition EnumCondition { get; set; }

        public MatchResultModel() { }

        public MatchResultModel(string id, string gameTime, string leagues, string homeTeam, string awayTeam, string homeScore, string awayScore, EnumCondition enumCondition, MatchDetailModel detail)
        {
            Id = id;
            GameTime = gameTime;
            Leagues = leagues;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
            EnumCondition = enumCondition;
            Detail = detail;
        }
    }
}
