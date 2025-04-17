using CaoHub.Web.Areas.Facturio.Services;
using CaoHub.Web.Areas.Facturio.ViewModels.Stores;
using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CaoHub.Web.Areas.Facturio.Controllers
{
    [Area("Facturio")]
    public class StoresController(
        StoreService storeService,
        StoreCategoryService storeCategoryService,
        IStringLocalizer<SharedResource> localizer) : Controller
    {
        private readonly StoreService _storeService = storeService;

        private readonly StoreCategoryService _storeCategoryService = storeCategoryService;

        private readonly IStringLocalizer _localizer = localizer;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _storeService.GetListAsync();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var storeCategorySelectList = await _storeCategoryService.GetSelectListAsync();

            return View(new StoreEditViewModel
            {
                StoreCategories = storeCategorySelectList,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                viewModel.StoreCategories = await _storeCategoryService.GetSelectListAsync();
                return View(viewModel);
            }

            await _storeService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeService.GetAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            var storeCategorySelectList = await _storeCategoryService.GetSelectListAsync();

            return View(new StoreEditViewModel
            {
                Id = store.Id,
                Name = store.Name,
                StoreCategoryId = store.StoreCategoryId,
                StoreCategories = storeCategorySelectList,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StoreEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                viewModel.StoreCategories = await _storeCategoryService.GetSelectListAsync();
                return View(viewModel);
            }

            await _storeService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _storeService.GetAsync(id);

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
            await _storeService.DeactivateAsync(id);

            return RedirectToAction("Index");
        }

        private async Task ValidateAsync(StoreEditViewModel viewModel)
        {
            if (viewModel.Name != null &&
              await _storeService.ExistsAsync(viewModel.Name, viewModel.Id))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    _localizer["Facturio_Stores_Name_ErrorExists"]);
            }

            if (viewModel.StoreCategoryId != null && 
                !await _storeCategoryService.ExistsAsync(viewModel.StoreCategoryId.Value))
            {
                ModelState.AddModelError(
                    nameof(viewModel.StoreCategoryId),
                    _localizer["Facturio_StoreCategories_ErrorNotExists"]);
            }
        }
    }
}
