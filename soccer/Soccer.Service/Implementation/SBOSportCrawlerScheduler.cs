using Soccer.Service.Interface;

namespace Soccer.Service.Implementation
{
    public class SBOSportCrawlerScheduler
    {
        ISBOMatchResultService _sBOMatchResultService;

        public SBOSportCrawlerScheduler(ISBOMatchResultService sBOMatchResultService)
        {
            _sBOMatchResultService = sBOMatchResultService;
        }
        public void UpdateResultDetailHistoryTable()
        {
            _sBOMatchResultService.UpdateResultDetailHistoryTableAsync();
        }
    }
}
