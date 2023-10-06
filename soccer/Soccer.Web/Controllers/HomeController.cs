using Microsoft.AspNetCore.Mvc;
using Soccer.Models;
using Soccer.Service.Interface;

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
            List<MatchResultModel> results = _service.GetAllMatchResults();
            return View(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("detail")]
        public JsonResult Detail(string id) 
        {
            MatchDetailModel matchDetailModel = _service.GetMatchDetailModel(id);
            return Json(matchDetailModel);
        }
    }
}