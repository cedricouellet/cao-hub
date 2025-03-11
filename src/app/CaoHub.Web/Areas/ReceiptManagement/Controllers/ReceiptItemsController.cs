using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class ReceiptItemsController : Controller
    {
        public IActionResult Index(int receiptId)
        {
            // TODO
            return View();
        }

        public IActionResult Create(int receiptId)
        {
            // TODO
            return View();
        }

        [HttpPost]
        public IActionResult Create(int receiptId, object todoBody)
        {
            // TODO
            return RedirectToAction(nameof(Index), new { receiptId = receiptId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // TODO
            int receiptId = 0;
            return RedirectToAction(nameof(Index), new { receiptId = receiptId });
        }
    }
}
