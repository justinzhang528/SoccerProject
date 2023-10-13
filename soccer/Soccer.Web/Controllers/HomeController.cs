using Microsoft.AspNetCore.Mvc;
using Soccer.Repository.Models;
using Soccer.Service.Interface;

namespace Soccer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchResultService _service;

        public HomeController(IMatchResultService service)
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