using NLog;
using soccer.Controllers;
using Soccer.Models;

namespace Soccer.Services
{
    public class SoccerService : ISoccerService
    {
        ResultBuilder resultBuilder;
        
        public SoccerService()
        {
            resultBuilder = new ResultBuilder("https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8");
        }

        public void GenerateResult()
        {
            List<Result> results = resultBuilder.GenerateResults();
            Logger logger = LogManager.GetLogger("resultHistory");
            string msg = "";
            foreach (Result result in results)
            {
                msg += result.GameTime + ",";
                msg += result.Leagues + ",";
                msg += result.HomeTeam + " vs " + result.AwayTeam + ",";
                if (result.Status == 1)
                {
                    msg += result.HomeScore + ",";
                    msg += result.AwayScore + "\n";
                    msg += "Detail:" + "\n";
                    msg += result.Detail.Teams[0] + " teams " + result.Detail.Teams[1] + "\n";
                    msg += result.Detail.FirstHalf[0] + " firstHalf " + result.Detail.FirstHalf[1] + "\n";
                    msg += result.Detail.SecondHalf[0] + " secondHalf " + result.Detail.SecondHalf[1] + "\n";
                    msg += result.Detail.RegularTime[0] + " regularTime " + result.Detail.RegularTime[1] + "\n";
                    msg += result.Detail.Corners[0] + " corners " + result.Detail.Corners[1] + "\n";
                    msg += result.Detail.Penalties[0] + " penalties " + result.Detail.Penalties[1] + "\n";
                    msg += result.Detail.YellowCards[0] + " yellowCards " + result.Detail.YellowCards[1] + "\n";
                    msg += result.Detail.RedCards[0] + " redCards " + result.Detail.RedCards[1] + "\n";
                    msg += result.Detail.FirstHalf[0] + " firstET " + result.Detail.FirstHalf[1] + "\n";
                    msg += result.Detail.SecondHalf[0] + " secondET " + result.Detail.SecondHalf[1] + "\n";
                    msg += result.Detail.PenaltiesShootout[0] + " penaltiesShootout " + result.Detail.PenaltiesShootout[1] + "\n";
                }
                else if (result.Status == 0)
                {
                    msg += "Cancelled\n";
                }
                msg += "\n--------------------------------------------------------------------------------------------\n";
            }
            logger.Info(msg);
        }

        public void UpdateDatabase()
        {
            
        }
    }
}
