using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class TaxesController(TaxService taxService) : Controller
    {
        private readonly TaxService _taxService = taxService;

        public async Task<IActionResult> Index()
        {
            var viewModel = await _taxService.GetListAsync();
            
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TaxCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxCreateViewModel viewModel)
        {
            if (viewModel.Name != null && 
                await _taxService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(TaxCreateViewModel.Name),
                    "A tax with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _taxService.CreateAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var tax = await _taxService.GetAsync(id);

            return View(tax);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleted = await _taxService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
