using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface ISBOMatchResultBuilder
    {
        public Task Build();
        public List<SBOMatchResultModel> GetSBOMatchResults();

        public List<SBOMatchDetailModel> GetSBOMatchDetails();
    }
}