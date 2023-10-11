namespace Soccer.Repository.Models
{
    public class SBOMatchDetailModel
    {
        public string Id { get; set; }
        public string GoalType { get; set; }
        public string FirstHalfScore { get; set; }
        public string FullTimeScore { get; set; }
        public string Code { get; set; }

        public SBOMatchDetailModel() { }

        public SBOMatchDetailModel(string id, string goalType, string firstHalfScore, string fullTimeScore, string code)
        {
            Id = id;
            GoalType = goalType;
            FirstHalfScore = firstHalfScore;
            FullTimeScore = fullTimeScore;
            Code = code;
        }
    }
}
