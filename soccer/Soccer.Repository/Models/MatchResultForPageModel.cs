using Soccer.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer.Repository.Models
{
    public class MatchResultForPageModel
    {
        public string Id { get; set; }
        public string MatchTime { get; set; }
        public string League { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string BTIHomeScore { get; set; }
        public string BTIAwayScore { get; set; }
        public string SBOHomeScore { get; set; }
        public string SBOAwayScore { get; set; }
        public MatchDetailModel Detail { get; set; }
        public EnumCondition Condition { get; set; }

        public MatchResultForPageModel() { }

        public MatchResultForPageModel(string id, string gameTime, string leagues, string homeTeam, string awayTeam, string bTIHomeScore, string sBOHomeScore, string bTIAwayScore, string sBOAwayScore, EnumCondition condition, MatchDetailModel detail)
        {
            Id = id;
            MatchTime = gameTime;
            League = leagues;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            BTIHomeScore = bTIHomeScore;
            BTIAwayScore = bTIAwayScore;
            SBOHomeScore = sBOHomeScore;
            SBOAwayScore = sBOAwayScore;
            Condition = condition;
            Detail = detail;
        }
    }
}
