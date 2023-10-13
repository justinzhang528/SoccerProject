using Soccer.Service.Interface;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using Microsoft.Extensions.Configuration;

namespace Soccer.Service.Implementation
{
    public class MatchResultService : IMatchResultService
    {
        IMatchResultRepository _matchResultRepository;
        IMatchResultBuilder _matchResultBuilder;
        IConfiguration _configuration;

        public MatchResultService(IMatchResultRepository soccerRepository, IMatchResultBuilder builder, IConfiguration configuration)
        {
            _matchResultRepository = soccerRepository;
            _matchResultBuilder = builder;
            _configuration = configuration;
            _matchResultBuilder.SetURL(_configuration["URL:BTISport"]);
        }

        public void UpdateResultDetailHistoryTable()
        {
            _matchResultRepository.UpdateResultDetailHistoryTable(_matchResultBuilder.GenerateResults());
        }

        public List<MatchResultModel> GetAllMatchResults()
        {
            return _matchResultRepository.GetAllMatchResults();
        }

        public MatchDetailModel GetMatchDetailModel(string id)
        {
            return _matchResultRepository.GetMatchDetailById(id);
        }
    }
}
