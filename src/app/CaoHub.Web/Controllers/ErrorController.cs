using CaoHub.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CaoHub.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("error/{statusCode:int?}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpError(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status404NotFound => View("NotFound"),
                StatusCodes.Status401Unauthorized => View("Unauthorized"),
                StatusCodes.Status403Forbidden => View("Forbidden"),
                _ => View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }),
            };
        }
    }
}
