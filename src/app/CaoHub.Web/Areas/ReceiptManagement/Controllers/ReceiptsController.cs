using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class ReceiptsController(
        ReceiptService receiptService, 
        PersonService personService, 
        StoreService storeService,
        StoreCategoryService storeCategoryService) : Controller
    {
        public IActionResult Index()
        {
            // TODO
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            // TODO
            return View(new ReceiptCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReceiptCreateViewModel viewModel)
        {
            // TODO
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // TODO
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            // TODO
            return RedirectToAction(nameof(Index));
        }
    }
}
