using Soccer.Models;

namespace Soccer.Services
{
    public interface ISoccerService
    {
        void GenerateMatchResult();
        List<MatchResult> GetAllMatchResults();
    }
}
