using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Stores;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class StoresController(
        StoreService storeService, 
        StoreCategoryService storeCategoryService) : Controller
    {
        private readonly StoreService _storeService = storeService;

        private readonly StoreCategoryService _storeCategoryService = storeCategoryService;

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
            
            return View(new StoreCreateViewModel
            {
                StoreCategories = storeCategorySelectList,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreCreateViewModel viewModel)
        {
            if (viewModel.Name != null && 
                await _storeService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name), 
                    "A store with this name already exists.");
            }

            if (viewModel.StoreCategoryId != null && 
                !await _storeCategoryService.ExistsAsync(viewModel.StoreCategoryId.Value))
            {
                ModelState.AddModelError(
                    nameof(viewModel.StoreCategoryId),
                    "The selected store category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                viewModel.StoreCategories = await _storeCategoryService.GetSelectListAsync();
                return View(viewModel);
            }

            await _storeService.CreateAsync(viewModel);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleted = await _storeService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
