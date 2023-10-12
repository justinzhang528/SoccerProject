namespace Soccer.Repository.Models
{
    public class SBOMatchResultModel
    {
        public string Id { get; set; }
        public string League { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string MatchTime { get; set; }
        public string HomeFirstHalfScore { get; set; }
        public string AwayFirstHalfScore { get; set; }
        public string HomeFullTimeScore { get; set; }
        public string AwayFullTimeScore { get; set; }
        public string IsShowMoreData { get; set; }

        public SBOMatchResultModel() { }

        public SBOMatchResultModel(string id, string league, string homeTeam, string awayTeam, string matchTime, string homeFirstHalfScore, string awayFirstHalfScore, string homefullTimeScore, string awayFullTimeScore, string isShowMoreData) 
        {
            Id = id;
            League = league;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            MatchTime = matchTime;
            HomeFirstHalfScore = homeFirstHalfScore;
            AwayFirstHalfScore = awayFirstHalfScore;
            HomeFullTimeScore = homefullTimeScore;
            AwayFullTimeScore = awayFullTimeScore;
            IsShowMoreData = isShowMoreData;
        }
    }
}
