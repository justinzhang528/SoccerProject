using Soccer.Models;

namespace Soccer.Service.Interface
{
    public interface ISoccerService
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResultModel> GetAllMatchResults();

        MatchDetailModel GetMatchDetailModel(string id);
    }
}
