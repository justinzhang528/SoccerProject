using Soccer.Common.Utils;

namespace Soccer.Repository.Models
{
    public class MatchResultModel
    {
        public string Id { get; set; }
        public string MatchTime { get; set; }
        public string League { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public MatchDetailModel Detail { get; set; }
        public EnumCondition Condition { get; set; }

        public MatchResultModel() { }

        public MatchResultModel(string id, string gameTime, string leagues, string homeTeam, string awayTeam, string homeScore, string awayScore, EnumCondition condition, MatchDetailModel detail)
        {
            Id = id;
            MatchTime = gameTime;
            League = leagues;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Condition = condition;
            Detail = detail;
        }
    }
}
