using Microsoft.Extensions.Configuration;
using Soccer.Repository.Interface;
using Soccer.Repository.Models;
using Soccer.Service.Interface;

namespace Soccer.Service.Implementation
{
    public class SBOMatchResultService : ISBOMatchResultService
    {
        ISBOMatchResultRepository _sBOMatchResultRepository;
        ISBOMatchResultBuilder _sBOMatchResultBuilder;
        IConfiguration _configuration;

        public SBOMatchResultService(ISBOMatchResultRepository sBOMatchResultRepository, ISBOMatchResultBuilder builder, IConfiguration configuration)
        {
            _sBOMatchResultRepository = sBOMatchResultRepository;
            _sBOMatchResultBuilder = builder;
            _configuration = configuration;
        }
        public void UpdateResultDetailHistoryTable()
        {
            _sBOMatchResultBuilder.Build();
            _sBOMatchResultRepository.UpdateResultDetailHistoryTable(_sBOMatchResultBuilder.GetSBOMatchResults(),_sBOMatchResultBuilder.GetSBOMatchDetails());
        }

        public List<SBOMatchResultModel> GetAllMatchResults()
        {
            return _sBOMatchResultRepository.GetAllMatchResults();
        }
    }
}
