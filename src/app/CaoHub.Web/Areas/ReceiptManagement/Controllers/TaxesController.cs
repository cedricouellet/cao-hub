using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Taxes;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [Route("[area]/[controller]")]
    public class TaxesController(TaxService taxService) : Controller
    {
        private readonly TaxService _taxService = taxService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _taxService.GetListAsync();
            
            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new TaxCreateViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxCreateViewModel viewModel)
        {
            if (viewModel.Name != null && 
                await _taxService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    "A tax with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _taxService.CreateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var tax = await _taxService.GetAsync(id);

            return View(tax);
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleted = await _taxService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
