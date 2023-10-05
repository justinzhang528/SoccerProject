using Microsoft.AspNetCore.Mvc;
using Soccer.Models;
using Soccer.Services;

namespace soccer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISoccerService _service;

        public HomeController(ISoccerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            List<MatchResult> results = _service.GetAllMatchResults();
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