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
            int cnt = 1;
            foreach (Result result in results)
            {
                msg += cnt + ".";
                msg += result.GameTime + ",";
                msg += result.Leagues + ",";
                msg += result.HomeTeam + " vs " + result.AwayTeam + ",";
                if (result.Status == 1)
                {
                    msg += result.HomeScore + ",";
                    msg += result.AwayScore + "\n";
                    msg += "Detail:" + "\n";
                    msg += result.HomeTeam + " teams " + result.AwayTeam + "\n";
                    msg += result.Detail.FirstHalf_H + " firstHalf " + result.Detail.FirstHalf_A + "\n";
                    msg += result.Detail.SecondHalf_H + " secondHalf " + result.Detail.SecondHalf_A + "\n";
                    msg += result.Detail.RegularTime_H + " regularTime " + result.Detail.RegularTime_A + "\n";
                    msg += result.Detail.Corners_H + " corners " + result.Detail.Corners_A + "\n";
                    msg += result.Detail.Penalties_H + " penalties " + result.Detail.Penalties_A + "\n";
                    msg += result.Detail.YellowCards_H + " yellowCards " + result.Detail.YellowCards_A + "\n";
                    msg += result.Detail.RedCards_H + " redCards " + result.Detail.RedCards_A + "\n";
                    msg += result.Detail.FirstHalf_H + " firstET " + result.Detail.FirstHalf_A + "\n";
                    msg += result.Detail.SecondHalf_H + " secondET " + result.Detail.SecondHalf_A + "\n";
                    msg += result.Detail.PenaltiesShootout_H + " penaltiesShootout " + result.Detail.PenaltiesShootout_A + "\n";
                }
                else if (result.Status == 0)
                {
                    msg += "Cancelled\n";
                }
                msg += "\n--------------------------------------------------------------------------------------------\n";
                cnt++;
            }
            logger.Info(msg);
        }

        public void UpdateDatabase()
        {
            
        }
    }
}
