using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface ISBOMatchResultBuilder
    {
        public void Build();
        public List<SBOMatchResultModel> GetSBOMatchResults();
        public List<SBOMatchDetailModel> GetSBOMatchDetails();

    }
}