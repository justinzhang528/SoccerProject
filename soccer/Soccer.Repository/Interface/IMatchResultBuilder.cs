using Soccer.Repository.Models;

namespace Soccer.Repository.Interface
{
    public interface IMatchResultBuilder
    {
        public void SetURL(string url);
        public List<MatchResultModel> GenerateResults();
    }
}
