using Soccer.Models;

namespace Soccer.Service.Interface
{
    public interface IBtiMatchResultService
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResultModel> GetAllMatchResults();

        MatchDetailModel GetMatchDetailModel(string id);
    }
}
