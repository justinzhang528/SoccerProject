using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Soccer.Models;
using Soccer.Services;

namespace soccer.Controllers
{
    public class HomeController : Controller
    {
        private ISoccerService _service = new SoccerService();
        public IActionResult Index()
        {
            List<Result> results = _service.GetAllResult();
            return View(results);
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
            return "Get result Done!";
        }
    }
}