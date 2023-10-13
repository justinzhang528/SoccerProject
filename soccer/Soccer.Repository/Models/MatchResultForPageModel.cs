using Soccer.Common.Utils;

namespace Soccer.Repository.Models
{
    public class MatchResultForPageModel
    {
        public string Id { get; set; }
        public string MatchTime { get; set; }
        public string BTILeague { get; set; }
        public string BTIHomeTeam { get; set; }
        public string BTIAwayTeam { get; set; }
        public string SBOLeague { get; set; }
        public string SBOHomeTeam { get; set; }
        public string SBOAwayTeam { get; set; }
        public string BTIHomeScore { get; set; }
        public string BTIAwayScore { get; set; }
        public string SBOHomeScore { get; set; }
        public string SBOAwayScore { get; set; }
        public EnumCondition Condition { get; set; }
    }
}