using Soccer.Models;

namespace Soccer.Services
{
    public interface ISoccerService
    {
        void GenerateResult();
        List<Result> GetAllResult();
    }
}
