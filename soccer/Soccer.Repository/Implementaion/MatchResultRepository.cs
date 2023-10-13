using Dapper;
using Microsoft.Extensions.Configuration;
using Soccer.Common.Utils;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using System.Data;

namespace Soccer.Repository.Implementaion
{
    public class MatchResultRepository : BaseRepository, IMatchResultRepository
    {
        public MatchResultRepository(IConfiguration configuration):base(configuration) { }

        public void UpdateResultDetailHistoryTable(List<MatchResultModel> results)
        {
            List<MatchDetailModel> details = new List<MatchDetailModel>();
            DataTable results_dt = ToDataTable(results);
            results_dt.Columns.Remove("Detail");

            foreach (MatchResultModel result in results)
            {
                if (result.Condition == EnumCondition.Normal)
                {
                    details.Add(result.Detail);
                }
            }
            DataTable details_dt = ToDataTable(details);
            var parameterObject = new
            {
                Results = results_dt.AsTableValuedParameter("dbo.MatchResultType"),
                Details = details_dt.AsTableValuedParameter("dbo.MatchDetailType")
            };
            UpdateAll("Soccer_MatchResult_UpdateMatchResultDetailHistory_v1", parameterObject);
        }

        public List<MatchResultModel> GetAllMatchResults()
        {
            return Query<MatchResultModel>("Soccer_MatchResult_GetAllMatchResults_v1");
        }

        public MatchDetailModel GetMatchDetailById(string id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            List<MatchDetailModel> matchDetailModels = Query<MatchDetailModel>("Soccer_MatchResult_GetMatchDetailById_v1", parameters);
            if(matchDetailModels.Count > 0)
                return matchDetailModels[0];
            return null;
        }
    }
}
