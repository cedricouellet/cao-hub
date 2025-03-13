using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [Route("[area]/[controller]")]
    public class ProductsController(ProductService productService) : Controller
    {
        private readonly ProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _productService.GetListAsync();

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new ProductCreateViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
        {
            if (viewModel.Name != null &&
                await _productService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    "A product with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _productService.CreateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _productService.GetAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
