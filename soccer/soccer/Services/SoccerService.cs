using Dapper;
using NLog;
using Soccer.Models;
using Soccer.Utils;

namespace Soccer.Services
{
    public class SoccerService : ISoccerService
    {
        ResultBuilder resultBuilder;
        DBConnUtil dBConnUtil;
        
        public SoccerService()
        {
            resultBuilder = new ResultBuilder("https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8");
            dBConnUtil = new DBConnUtil();
        }

        public void GenerateResult()
        {
            List<Result> results = resultBuilder.GenerateResults();
            //Logger logger = LogManager.GetLogger("resultHistory");
            foreach (Result result in results)
            {
                Result resultRow = GetResultById(result.Id);

                // 如果DB裏不存在則新增Result
                if (resultRow == null)
                {
                    AddResult(result);

                    // 如果是正常狀態下，則需要新增Detail資料
                    if(result.Condition == 1)
                    {
                        AddDetail(result.Detail);
                    }
                }
                // 如果DB裏已經存在，則檢查是否需要更新Detail和Result
                else
                {
                    // 如果在正常狀態下
                    if (result.Condition == 1) 
                    {

                    }
                }
            }
            //logger.Info(msg);
        }

        public List<Result> GetAllResult()
        {
            List<Result> results;
            using (var connection = dBConnUtil.GetConnection())
            {
                results = connection.Query<Result>("spGetAllResults", commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            foreach (Result result in results)
            {
                if(result.Condition == 1)
                {
                    result.Detail = GetDetailById(result.Id);
                }
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
        public void AddHistory(Detail detail)
        {
            using (var connection = dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ResultId", detail.Id);
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
