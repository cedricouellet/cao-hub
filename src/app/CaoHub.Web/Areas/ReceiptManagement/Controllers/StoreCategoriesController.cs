using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.StoreCategories;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [Route("[area]/[controller]")]
    public class StoreCategoriesController(StoreCategoryService storeCategoryService) : Controller
    {
        private readonly StoreCategoryService _storeCategoryService = storeCategoryService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _storeCategoryService.GetListAsync();

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new StoreCategoryCreateViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreCategoryCreateViewModel viewModel)
        {
            if (viewModel.Name != null && 
                await _storeCategoryService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name), 
                    "A store category with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _storeCategoryService.CreateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _storeCategoryService.GetAsync(id);

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
            var deleted = await _storeCategoryService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
