using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface IMatchResultService
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResultModel> GetAllMatchResults();

        MatchDetailModel GetMatchDetailModel(string id);
    }
}
