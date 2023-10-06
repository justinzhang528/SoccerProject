using Soccer.Models;
using Soccer.Service.Interface;
using Soccer.Repository.Interface;
using Soccer.Repository.Implementaion;

namespace Soccer.Services
{
    public class SoccerService : ISoccerService
    {
        ISoccerRepository _soccerRepository;

        public SoccerService(ISoccerRepository soccerRepository)
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
