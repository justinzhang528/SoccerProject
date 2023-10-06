using Dapper;
using Microsoft.Extensions.Configuration;
using Soccer.Common.Utils;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using System.Data;
using System.Reflection;

namespace Soccer.Repository.Implementaion
{
    public class MatchResultRepository : BaseRepository, IMatchResultRepository
    {
        private readonly IMatchResultBuilder _matchResultBuilder;

        public MatchResultRepository(IMatchResultBuilder matchResultBuilder, IConfiguration configuration): base(configuration)
        {
            _matchResultBuilder = matchResultBuilder;
            _matchResultBuilder = matchResultBuilder;
            _matchResultBuilder.SetURL(configuration["URL:soccer"]);
        }

        public void UpdateResultDetailHistoryTable()
        {
            List<MatchResultModel> results = _matchResultBuilder.GenerateResults();
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
            UpdateAll("Soccer_MatchResult_UpdateAllMatchResults_v1", new { Results = results_dt.AsTableValuedParameter("dbo.MatchResultType") });
            UpdateAll("Soccer_MatchResult_UpdateAllMatchDetails_v1", new { Details = details_dt.AsTableValuedParameter("dbo.MatchDetailType") });
        }

        public List<MatchResultModel> GetAllMatchResults()
        {
            List<MatchResultModel> results;
            List<MatchDetailModel> details;
            results = Query<MatchResultModel>("Soccer_MatchResult_GetAllMatchResults_v1");
            details = Query<MatchDetailModel>("Soccer_MatchResult_GetAllMatchDetails_v1");

            foreach (MatchResultModel result in results)
            {
                if (result.Condition == EnumCondition.Normal)
                {
                    foreach (MatchDetailModel detail in details)
                    {
                        if (result.Id == detail.Id)
                        {
                            result.Detail = detail;
                        }
                    }
                }
            }
            return results;
        }

        public MatchDetailModel GetMatchDetailModel(string id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            List<MatchDetailModel> matchDetailModels = Query<MatchDetailModel>("Soccer_MatchResult_GetDetailById_v1", parameters);
            if(matchDetailModels.Count > 0)
                return matchDetailModels[0];
            return null;
        }
    }
}
