using Dapper;
using Microsoft.Extensions.Configuration;
using Soccer.Common.Utils;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using System.Data;

namespace Soccer.Repository.Implementaion
{
    public class CookiesRepository : BaseRepository, ICookiesRepository
    {
        public CookiesRepository(IConfiguration configuration) : base(configuration) { }

        public void UpdateCookies(List<CookiesModel> cookies)
        {
            DataTable results_dt = ToDataTable(cookies);
            var parameterObject = new
            {
                Cookies = results_dt.AsTableValuedParameter("dbo.CookiesType")
            };
            UpdateAll("Soccer_MatchResult_UpdateCookies_v1", parameterObject);
        }

        public CookiesModel GetCookiesByCondition(string website, string name)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Website", website);
            parameters.Add("Name", name);
            List<CookiesModel> results = Query<CookiesModel>("[Soccer_MatchResult_GetCookiesByCondition_v1]", parameters);
            if (results.Count > 0)
            {
                return results[0];
            }
            return null;
        }
    }
}
