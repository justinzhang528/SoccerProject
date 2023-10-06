using Soccer.Repository.Models;

namespace Soccer.Repository.Interface
{
    public interface IMatchResultRepository
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResultModel> GetAllMatchResults();
        MatchDetailModel GetMatchDetailModel(string id);
    }
}
