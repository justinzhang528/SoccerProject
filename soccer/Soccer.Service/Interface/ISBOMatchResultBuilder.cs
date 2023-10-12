using Soccer.Repository.Models;

namespace Soccer.Service.Interface
{
    public interface ISBOMatchResultBuilder
    {
        Task<List<SBOMatchDetailModel>> GenerateSBOMatchDetailsAsync(string eventId);
        Task<List<SBOMatchResultModel>> GenerateSBOMatchResultsAsync();
    }
}