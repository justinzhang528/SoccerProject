using Soccer.Service.Interface;

namespace Soccer.Service.Implementation
{
    public class SBOCookiesUpdateScheduler
    {
        readonly ICookiesService _cookiesService;

        public SBOCookiesUpdateScheduler(ICookiesService cookiesService)
        {
            _cookiesService = cookiesService;
        }

        public void UpdateSBOCookies()
        {
            _cookiesService.UpdateSBOCookies();
        }
    }
}
