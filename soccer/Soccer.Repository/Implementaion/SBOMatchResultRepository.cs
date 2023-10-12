using Dapper;
using Microsoft.Extensions.Configuration;
using Soccer.Common.Utils;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using System.Data;

namespace Soccer.Repository.Implementaion
{
    public class SBOMatchResultRepository : BaseRepository, ISBOMatchResultRepository
    {
        public SBOMatchResultRepository(IConfiguration configuration) : base(configuration) { }

        public void UpdateResultDetailHistoryTable(List<SBOMatchResultModel> results, List<SBOMatchDetailModel> details)
        {
            DataTable results_dt = ToDataTable(results);
            DataTable details_dt = ToDataTable(details);
            var parameterObject = new
            {
                Results = results_dt.AsTableValuedParameter("dbo.SBOMatchResultType"),
                Details = details_dt.AsTableValuedParameter("dbo.SBOMatchDetailType")
            };
            UpdateAll("Soccer_SBOMatchResult_UpdateSBOMatchResultDetailHistory_v1", parameterObject);
        }

        public List<SBOMatchResultModel> GetAllMatchResults()
        {
            List<SBOMatchResultModel> results;
            results = Query<SBOMatchResultModel>("[Soccer_SBOMatchResult_GetAllSBOMatchResults_v1]");
            return results;
        }
    }
}
