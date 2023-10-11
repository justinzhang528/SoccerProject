namespace Soccer.Repository.Models
{
    public class SBOMatchResultModel
    {
        public string Id { get; set; }
        public string League { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public string DateTime { get; set; }
        public string FirstHalfScore { get; set; }
        public string FullTimeScore { get; set; }
        public string IsShowMoreData { get; set; }

        public SBOMatchResultModel() { }

        public SBOMatchResultModel(string id, string league, string teamA, string teamB, string dateTime, string firstHalfScore, string fullTimeScore, string isShowMoreData) 
        {
            Id = id;
            League = league;
            TeamA = teamA;
            TeamB = teamB;
            DateTime = dateTime;
            FirstHalfScore = firstHalfScore;
            FullTimeScore = fullTimeScore;
            IsShowMoreData = isShowMoreData;
        }
    }
}
