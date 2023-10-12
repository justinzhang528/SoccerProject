namespace Soccer.Repository.Models
{
    public class SBOMatchDetailModel
    {
        public string Id { get; set; }
        public string MarketType { get; set; }
        public string HomeFirstHalfScore { get; set; }
        public string AwayFirstHalfScore { get; set; }
        public string HomeFullTimeScore { get; set; }
        public string AwayFullTimeScore { get; set; }
        public string Code { get; set; }

        public SBOMatchDetailModel() { }

        public SBOMatchDetailModel(string id, string marketType, string homeFirstHalfScore, string awayFirstHalfScore, string homefullTimeScore, string awayFullTimeScore, string code)
        {
            Id = id;
            MarketType = marketType;
            HomeFirstHalfScore = homeFirstHalfScore;
            AwayFirstHalfScore = awayFirstHalfScore;
            HomeFullTimeScore = homefullTimeScore;
            AwayFullTimeScore = awayFullTimeScore;
            Code = code;
        }
    }
}
