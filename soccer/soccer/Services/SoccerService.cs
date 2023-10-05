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
        IConfiguration _configuration;
        
        public SoccerService(DBConnUtil dBConnUtil, MatchResultBuilder matchResultBuilder, IConfiguration configuration)
        {
            _matchResultBuilder = matchResultBuilder;
            _configuration = configuration;
            _dBConnUtil = dBConnUtil;
            _matchResultBuilder = matchResultBuilder;
            _matchResultBuilder.URL = configuration.GetValue<string>("URL:soccer");
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
            DataTable results_dt = ToDataTable(results);
            results_dt.Columns.Remove("Detail");

            foreach (MatchResult result in results)
            {
                if(result.Condition == ConditionInfo.Normal)
                {
                    details.Add(result.Detail);
                }
            }
            DataTable details_dt = ToDataTable(details);
            _dBConnUtil.UpdateAll("Soccer_MatchResult_UpdateAllMatchResults_v1", new { Results = results_dt.AsTableValuedParameter("dbo.MatchResultType") });
            _dBConnUtil.UpdateAll("Soccer_MatchResult_UpdateAllMatchDetails_v1", new { Details = details_dt.AsTableValuedParameter("dbo.MatchDetailType") });
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

    }
}
