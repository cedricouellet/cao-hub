using CaoHub.Web.Areas.Facturio.Services;
using CaoHub.Web.Areas.Facturio.ViewModels.StoreCategories;
using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CaoHub.Web.Areas.Facturio.Controllers
{
    [Area("Facturio")]
    public class StoreCategoriesController(
        StoreCategoryService storeCategoryService,
        IStringLocalizer<SharedResource> localizer) : Controller
    {
        private readonly StoreCategoryService _storeCategoryService = storeCategoryService;

        private readonly IStringLocalizer _localizer = localizer;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _storeCategoryService.GetListAsync();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new StoreCategoryEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreCategoryEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _storeCategoryService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var storeCategory = await _storeCategoryService.GetAsync(id);

            if (storeCategory == null)
            {
                return NotFound();
            }

            return View(new StoreCategoryEditViewModel
            {
                Id = storeCategory.Id,
                Name = storeCategory.Name,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StoreCategoryEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _storeCategoryService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _storeCategoryService.GetAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _storeCategoryService.DeactivateAsync(id);

            return RedirectToAction("Index");
        }

        private async Task ValidateAsync(StoreCategoryEditViewModel viewModel)
        {
            if (viewModel.Name != null &&
                await _storeCategoryService.ExistsAsync(viewModel.Name, viewModel.Id))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    _localizer["Facturio_StoreCategories_Name_ErrorExists"]);
            }
        }
    }
}
