using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.Facturio.Controllers
{
    [Area("Facturio")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
