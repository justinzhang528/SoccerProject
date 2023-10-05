using Dapper;
using Soccer.Models;
using Soccer.Utils;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;

namespace Soccer.Services
{
    public class SoccerService : ISoccerService
    {
        MatchResultBuilder _matchResultBuilder;
        DBConnUtil _dBConnUtil;
        
        public SoccerService(DBConnUtil dBConnUtil)
        {
            _matchResultBuilder = new MatchResultBuilder("https://bti-results.bsportsasia.com/?ns=prod20082-23705321.bti-sports.io&locale=en&tzoffset=8");
            _dBConnUtil = dBConnUtil;
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(ConditionInfo))
                    dataTable.Columns.Add(prop.Name, typeof(Int32));
                else
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public void UpdateResultDetailHistoryTable()
        {
            List<MatchResult> results = _matchResultBuilder.GenerateResults();
            List<MatchDetail> details = new List<MatchDetail>();
            var results_dt = ToDataTable(results);
            results_dt.Columns.Remove("Detail");

            foreach (MatchResult result in results)
            {
                if(result.Condition == ConditionInfo.Normal)
                {
                    details.Add(result.Detail);
                }
            }
            _dBConnUtil.UpdateAll("Soccer_MatchResult_UpdateAllMatchResults_v1", new { Results = results_dt.AsTableValuedParameter("dbo.MatchResultType") });
        }

        public List<MatchResult> GetAllMatchResults()
        {
            List<MatchResult> results;
            List<MatchDetail> details;
            results = _dBConnUtil.QueryAll<MatchResult>("Soccer_MatchResult_GetAllMatchResults_v1");
            details = _dBConnUtil.QueryAll<MatchDetail>("Soccer_MatchResult_GetAllMatchDetails_v1");

            foreach (MatchResult result in results)
            {
                if (result.Condition == ConditionInfo.Normal)
                {
                    foreach (MatchDetail detail in details)
                    {
                        if(result.Id == detail.Id)
                        {
                            result.Detail = detail;
                        }
                    }
                }
            }
            return results;
        }


        public MatchResult GetMatchResultById(string id)
        {
            MatchResult result;
            using (var connection = _dBConnUtil.GetConnection())
            { 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", id);
                result = connection.QuerySingleOrDefault<MatchResult>("spGetResultById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        public MatchDetail GetMatchDetailById(string id)
        {
            MatchDetail detail;
            using (var connection = _dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", id);

                detail = connection.QuerySingleOrDefault<MatchDetail>("spGetDetailById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return detail;
        }

        public void AddMatchResult(MatchResult result)
        {
            using (var connection = _dBConnUtil.GetConnection())
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

        public void AddMatchDetail(MatchDetail detail)
        {
            using (var connection = _dBConnUtil.GetConnection())
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
        public void AddHistory(MatchDetail detail)
        {
            using (var connection = _dBConnUtil.GetConnection())
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

        public void UpdateMatchResultById(MatchResult result)
        {
            using (var connection = _dBConnUtil.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", result.Id);
                parameters.Add("HomeScore", result.HomeScore);
                parameters.Add("AwayScore", result.AwayScore);
                parameters.Add("Condition", result.Condition);

                connection.Execute("spUpdateResultById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void UpdateMatchDetialById(MatchDetail detail)
        {
            using (var connection = _dBConnUtil.GetConnection())
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
        // 如何把 dbconnection and Dapper Query的程式碼包在一個 DBConnUtil.cs 裏，供services.cs來使用

    }
}
