using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Soccer.Services;

namespace soccer.Controllers
{
    public class HomeController : Controller
    {
        private ISoccerService _service = new SoccerService();
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
            //add schedule job
            RecurringJob.AddOrUpdate<ISoccerService>(x => x.GenerateResult(), "*/03 * * * *");
            return "Get result Done!";
        }
    }
}