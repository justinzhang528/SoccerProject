using Microsoft.AspNetCore.Mvc;
using Soccer.Models;
using Soccer.Service.Interface;

namespace Soccer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBtiMatchResultService _service;

        public HomeController(IBtiMatchResultService service)
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