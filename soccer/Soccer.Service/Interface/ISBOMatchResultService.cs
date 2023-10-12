using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface ISBOMatchResultService
    {
        List<SBOMatchResultModel> GetAllMatchResults();
        void UpdateResultDetailHistoryTable();
    }
}