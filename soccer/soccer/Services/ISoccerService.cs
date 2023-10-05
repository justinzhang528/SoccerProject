using Soccer.Models;

namespace Soccer.Services
{
    public interface ISoccerService
    {
        void UpdateResultDetailHistoryTable();
        List<MatchResult> GetAllMatchResults();
    }
}
