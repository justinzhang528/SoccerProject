using Soccer.Repository.Models;

namespace Soccer.Repository.Interface
{
    public interface ISBOMatchResultRepository
    {
        void UpdateResultDetailHistoryTable(List<SBOMatchResultModel> results, List<SBOMatchDetailModel> details);

        List<SBOMatchResultModel> GetAllMatchResults();
    }
}