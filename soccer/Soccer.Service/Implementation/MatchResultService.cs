using Soccer.Service.Interface;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;

namespace Soccer.Service.Implementation
{
    public class MatchResultService : IMatchResultService
    {
        IMatchResultRepository _soccerRepository;

        public MatchResultService(IMatchResultRepository soccerRepository)
        {
            _soccerRepository = soccerRepository;
        }

        public void UpdateResultDetailHistoryTable()
        {
            _soccerRepository.UpdateResultDetailHistoryTable();
        }

        public List<MatchResultModel> GetAllMatchResults()
        {
            return _soccerRepository.GetAllMatchResults();
        }

        public MatchDetailModel GetMatchDetailModel(string id)
        {
            return _soccerRepository.GetMatchDetailModel(id);
        }
    }
}
