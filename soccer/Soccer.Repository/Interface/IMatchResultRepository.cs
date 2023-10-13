using Soccer.Repository.Models;

namespace Soccer.Repository.Interface
{
    public interface IMatchResultRepository
    {
        void UpdateResultDetailHistoryTable(List<MatchResultModel> results);
        List<MatchResultModel> GetAllMatchResults();
        MatchDetailModel GetMatchDetailById(string id);
    }
}
