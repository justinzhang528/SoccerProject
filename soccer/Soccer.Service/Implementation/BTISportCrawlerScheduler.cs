using Soccer.Service.Interface;

namespace Soccer.Service.Implementation
{

    public class BTISportCrawlerScheduler
    {
        IMatchResultService _matchResultService;

        public BTISportCrawlerScheduler(IMatchResultService matchResultService)
        {
            _matchResultService = matchResultService;
        }
        public void UpdateResultDetailHistoryTable()
        {
            _matchResultService.UpdateResultDetailHistoryTable();
        }
    }
}
