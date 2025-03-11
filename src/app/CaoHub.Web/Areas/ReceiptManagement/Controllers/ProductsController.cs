using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class ProductsController(ProductService productService) : Controller
    {
        private readonly ProductService _productService = productService;

        public async Task<IActionResult> Index()
        {
            var viewModel = await _productService.GetListAsync();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
        {
            if (viewModel.Name != null &&
                await _productService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(ProductCreateViewModel.Name),
                    "A product with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _productService.CreateAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _productService.GetAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
