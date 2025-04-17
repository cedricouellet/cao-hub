using CaoHub.Web.Areas.Facturio.Services;
using CaoHub.Web.Areas.Facturio.ViewModels.Taxes;
using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CaoHub.Web.Areas.Facturio.Controllers
{
    [Area("Facturio")]
    public class TaxesController(
        TaxService taxService, 
        CalculationMethodService calculationMethodService,
        IStringLocalizer<SharedResource> localizer) : Controller
    {
        private readonly TaxService _taxService = taxService;

        private readonly CalculationMethodService _calculationMethodService = calculationMethodService;

        private readonly IStringLocalizer _localizer = localizer;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _taxService.GetListAsync();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var calculationMethodSelectList = _calculationMethodService.GetSelectList();

            return View(new TaxEditViewModel
            {
                CalculationMethods = calculationMethodSelectList,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                viewModel.CalculationMethods = _calculationMethodService.GetSelectList();
                return View(viewModel);
            }

            await _taxService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tax = await _taxService.GetAsync(id);

            if (tax == null)
            {
                return NotFound();
            }

            var calculationMethodSelectList = _calculationMethodService.GetSelectList();

            return View(new TaxEditViewModel
            {
                Id = tax.Id,
                Name = tax.Name,
                Description = tax.Description,
                Value = tax.Value,
                CalculationMethod = tax.CalculationMethod,
                CalculationMethods = calculationMethodSelectList,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaxEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                viewModel.CalculationMethods = _calculationMethodService.GetSelectList();
                return View(viewModel);
            }

            await _taxService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _taxService.GetAsync(id);

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
            await _taxService.DeactivateAsync(id);

            return RedirectToAction("Index");
        }

        private async Task ValidateAsync(TaxEditViewModel viewModel)
        {
            if (viewModel.Name != null && 
                await _taxService.ExistsAsync(viewModel.Name, viewModel.Id))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    _localizer["Facturio_Taxes_Name_ErrorExists"]);
            }

            if (viewModel.CalculationMethod != null && 
                !_calculationMethodService.Exists(viewModel.CalculationMethod.Value))
            {
                ModelState.AddModelError(
                    nameof(viewModel.CalculationMethod),
                    _localizer["Facturio_Taxes_CalculationMethods_ErrorNotExists"]);
            }
        }
    }
}
