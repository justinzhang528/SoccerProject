using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Soccer.Services;

namespace soccer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("Result")]
        public string Result() 
        {
            // add schedule job
            RecurringJob.AddOrUpdate<IServiceManagement>(x => x.UpdateDatabase(), "*/15 * * * * *");
            return "Done!";
        }
    }
}