using Soccer.Models;

namespace Soccer.Repository.Interface
{
    public interface ISoccerRepository
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResultModel> GetAllMatchResults();
        MatchDetailModel GetMatchDetailModel(string id);
    }
}
