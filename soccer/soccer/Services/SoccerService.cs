using Dapper;
using Microsoft.Data.SqlClient;
using NLog;
using Soccer.Models;
using Soccer.Utils;

namespace Soccer.Services
{
    public class SoccerService : ISoccerService
    {
        ResultBuilder resultBuilder;
        DBConnUtil dBConnUtil;
        
        public SoccerService(IConfiguration configuration)
        {
            resultBuilder = new ResultBuilder("https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8");
            dBConnUtil = new DBConnUtil(configuration);
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
                if (result.Condition == 1)
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
                else if (result.Condition == 0)
                {
                    msg += "Cancelled\n";
                }
                msg += "\n--------------------------------------------------------------------------------------------\n";
                cnt++;
            }
            logger.Info(msg);
        }


        public List<Result> GetAllResult()
        {
            List<Result> results;
            using (var connection = dBConnUtil.GetConnection())
            {
                results = connection.Query<Result>("spGetAllResults", commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return results;
        }


        public Result GetResultById(string id)
        {
            Result result;
            using (var connection = dBConnUtil.GetConnection())
            { 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", id);

                result = connection.QuerySingleOrDefault<Result>("spGetResultById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        public Detail GetDetailById(string id)
        {
            Detail detail;
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", id);

                detail = connection.QuerySingleOrDefault<Detail>("spGetDetailById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return detail;
        }

        public void AddResult(Result result)
        {
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", result.Id);
                parameters.Add("GameTime", result.GameTime);
                parameters.Add("Leagues", result.Leagues);
                parameters.Add("HomeTeam", result.HomeTeam);
                parameters.Add("AwayTeam", result.AwayTeam);
                parameters.Add("HomeScore", result.HomeScore);
                parameters.Add("AwayScore", result.AwayScore);
                parameters.Add("Condition", result.Condition);

                connection.Execute("spAddResult", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void AddDetail(Detail detail)
        {
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", detail.Id);
                parameters.Add("FirstHalf_H", detail.FirstHalf_H);
                parameters.Add("FirstHalf_A", detail.FirstHalf_A);
                parameters.Add("SecondHalf_H", detail.SecondHalf_H);
                parameters.Add("SecondHalf_A", detail.SecondHalf_A);
                parameters.Add("RegularTime_H", detail.RegularTime_H);
                parameters.Add("RegularTime_A", detail.RegularTime_A);
                parameters.Add("Corners_H", detail.Corners_H);
                parameters.Add("Corners_A", detail.Corners_A);
                parameters.Add("Penalties_H", detail.Penalties_H);
                parameters.Add("Penalties_A", detail.Penalties_A);
                parameters.Add("YellowCards_H", detail.YellowCards_H);
                parameters.Add("YellowCards_A", detail.YellowCards_A);
                parameters.Add("RedCards_H", detail.RedCards_H);
                parameters.Add("RedCards_A", detail.RedCards_A);
                parameters.Add("FirstET_H", detail.FirstET_H);
                parameters.Add("FirstET_A", detail.FirstET_A);
                parameters.Add("SecondET_H", detail.SecondET_H);
                parameters.Add("SecondET_A", detail.SecondET_A);
                parameters.Add("PenaltiesShootout_H", detail.PenaltiesShootout_H);
                parameters.Add("PenaltiesShootout_A", detail.PenaltiesShootout_A);

                connection.Execute("spAddDetail", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        // to do
        public void AddHistory(History history)
        {
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                
                connection.Execute("spAddHistory", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void UpdateResultById(Result result)
        {
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", result.Id);
                parameters.Add("HomeScore", result.HomeScore);
                parameters.Add("AwayScore", result.AwayScore);
                parameters.Add("Condition", result.Condition);

                connection.Execute("spUpdateResultById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void UpdateDetialById(Detail detail)
        {
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", detail.Id);
                parameters.Add("FirstHalf_H", detail.FirstHalf_H);
                parameters.Add("FirstHalf_A", detail.FirstHalf_A);
                parameters.Add("SecondHalf_H", detail.SecondHalf_H);
                parameters.Add("SecondHalf_A", detail.SecondHalf_A);
                parameters.Add("RegularTime_H", detail.RegularTime_H);
                parameters.Add("RegularTime_A", detail.RegularTime_A);
                parameters.Add("Corners_H", detail.Corners_H);
                parameters.Add("Corners_A", detail.Corners_A);
                parameters.Add("Penalties_H", detail.Penalties_H);
                parameters.Add("Penalties_A", detail.Penalties_A);
                parameters.Add("YellowCards_H", detail.YellowCards_H);
                parameters.Add("YellowCards_A", detail.YellowCards_A);
                parameters.Add("RedCards_H", detail.RedCards_H);
                parameters.Add("RedCards_A", detail.RedCards_A);
                parameters.Add("FirstET_H", detail.FirstET_H);
                parameters.Add("FirstET_A", detail.FirstET_A);
                parameters.Add("SecondET_H", detail.SecondET_H);
                parameters.Add("SecondET_A", detail.SecondET_A);
                parameters.Add("PenaltiesShootout_H", detail.PenaltiesShootout_H);
                parameters.Add("PenaltiesShootout_A", detail.PenaltiesShootout_A);

                connection.Execute("spUpdateDetialById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

    }
}
