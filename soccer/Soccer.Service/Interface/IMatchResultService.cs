using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface IMatchResultService
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResultForPageModel> GetAllMatchResults();

        MatchDetailModel GetMatchDetailModel(string id);
    }
}
