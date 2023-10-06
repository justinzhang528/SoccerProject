using Dapper;
using Microsoft.Extensions.Configuration;
using Soccer.Models;
using Soccer.Repository.Interface;
using System.Data;
using System.Reflection;
using Soccer.Common.Utils;

namespace Soccer.Repository.Implementaion
{
    public class MatchResultRepository : BaseRepository, IMatchResultRepository
    {
        private readonly IMatchResultBuilder _matchResultBuilder;

        public MatchResultRepository(IMatchResultBuilder matchResultBuilder,
            IConfiguration configuration) : base(configuration)
        {
            _matchResultBuilder = matchResultBuilder;
            _matchResultBuilder = matchResultBuilder;
            _matchResultBuilder.SetURL(configuration["URL:soccer"]);
        }

        public void UpdateResultDetailHistoryTable()
        {
            var results = _matchResultBuilder.GenerateResults();
            var details = new List<MatchDetailModel>();
            var resultDataTable = ToDataTable(results);
            resultDataTable.Columns.Remove("Detail");

            foreach (MatchResultModel result in results)
            {
                if (result.EnumCondition == EnumCondition.Normal)
                {
                    details.Add(result.Detail);
                }
            }

            var detailDataTable = ToDataTable(details);
            UpdateAll("Soccer_MatchResult_UpdateAllMatchResults_v1",
                new { Results = resultDataTable.AsTableValuedParameter("dbo.MatchResultType") });
            UpdateAll("Soccer_MatchResult_UpdateAllMatchDetails_v1",
                new { Details = detailDataTable.AsTableValuedParameter("dbo.MatchDetailType") });
        }

        public List<MatchResultModel> GetAllMatchResults()
        {
            List<MatchResultModel> results;
            List<MatchDetailModel> details;
            results = Query<MatchResultModel>("Soccer_MatchResult_GetAllMatchResults_v1");
            details = Query<MatchDetailModel>("Soccer_MatchResult_GetAllMatchDetails_v1");

            foreach (MatchResultModel result in results)
            {
                if (result.EnumCondition == EnumCondition.Normal)
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
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("Id", id);
            List<MatchDetailModel> matchDetailModels =
                Query<MatchDetailModel>("Soccer_MatchResult_GetDetailById_v1", parameters);
            if (matchDetailModels.Count > 0)
                return matchDetailModels[0];
            return null;
        }
    }
}