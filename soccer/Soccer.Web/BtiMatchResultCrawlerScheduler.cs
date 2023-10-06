using Soccer.Service.Interface;

public class BtiMatchResultCrawlerScheduler
{
    private readonly IBtiMatchResultService _btiMatchResultService;

    public BtiMatchResultCrawlerScheduler(IBtiMatchResultService btiMatchResultService)
    {
        _btiMatchResultService = btiMatchResultService;
    }

    public void Execute()
    {
        _btiMatchResultService.UpdateResultDetailHistoryTable();
    }
}