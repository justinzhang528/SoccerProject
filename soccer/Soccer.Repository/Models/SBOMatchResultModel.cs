namespace Soccer.Repository.Models
{
    public class SBOMatchResultModel
    {
        public string Id { get; set; }
        public string League { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string MatchTime { get; set; }
        public string FirstHalfScore { get; set; }
        public string FullTimeScore { get; set; }
        public string IsShowMoreData { get; set; }

        public SBOMatchResultModel() { }

        public SBOMatchResultModel(string id, string league, string homeTeam, string awayTeam, string matchTime, string firstHalfScore, string fullTimeScore, string isShowMoreData) 
        {
            Id = id;
            League = league;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            MatchTime = matchTime;
            FirstHalfScore = firstHalfScore;
            FullTimeScore = fullTimeScore;
            IsShowMoreData = isShowMoreData;
        }
    }
}
