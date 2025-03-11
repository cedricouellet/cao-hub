﻿using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class StoreCategoriesController(StoreCategoryService storeCategoryService) : Controller
    {
        private readonly StoreCategoryService _storeCategoryService = storeCategoryService;

        public async Task<IActionResult> Index()
        {
            var viewModel = await _storeCategoryService.GetListAsync();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new StoreCategoryCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreCategoryCreateViewModel viewModel)
        {
            if (viewModel.Name != null && 
                await _storeCategoryService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(StoreCategoryCreateViewModel.Name), 
                    "A store category with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _storeCategoryService.CreateAsync(viewModel);

            return RedirectToAction(nameof(Index));
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleted = await _storeCategoryService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
