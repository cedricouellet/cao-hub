using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CaoHub.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
