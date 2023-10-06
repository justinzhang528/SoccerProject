using Soccer.Models;
using Soccer.Service.Interface;
using Soccer.Repository.Interface;
using Soccer.Repository.Implementaion;

namespace Soccer.Services
{
    public class BtiMatchResultService : IBtiMatchResultService
    {
        private readonly IMatchResultRepository _matchResultRepository;

        public BtiMatchResultService(IMatchResultRepository matchResultRepository)
        {
            _matchResultRepository = matchResultRepository;
        }

        public void UpdateResultDetailHistoryTable()
        {
            
            _matchResultRepository.UpdateResultDetailHistoryTable();
        }

        public List<MatchResultModel> GetAllMatchResults()
        {
            return _matchResultRepository.GetAllMatchResults();
        }

        public MatchDetailModel GetMatchDetailModel(string id)
        {
            return _matchResultRepository.GetMatchDetailModel(id);
        }
    }
}
