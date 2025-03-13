using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.ReceiptItems;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [Route("[area]/receipts/{receiptId:int}/[controller]")]
    public class ReceiptItemsController(
        ReceiptService receiptService,
        ReceiptItemService receiptItemService,
        PersonService personService,
        TaxService taxService,
        ProductService productService) : Controller
    {
        private readonly ReceiptService _receiptService = receiptService;

        private readonly ReceiptItemService _receiptItemService = receiptItemService;

        private readonly PersonService _personService = personService;

        private readonly TaxService _taxService = taxService;

        private readonly ProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> Index(int receiptId)
        {
            var viewModel = await _receiptItemService.GetListAsync(receiptId);
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create(int receiptId)
        {
            var productSelectItems = await _productService.GetSelectListAsync();
            var taxSelectItems = await _taxService.GetSelectListAsync();
            var personSelectItems = await _personService.GetSelectListAsync();

            return View(new ReceiptItemCreateViewModel
            {
                ReceiptId = receiptId,
                Products = productSelectItems,
                Taxes = taxSelectItems,
                People = personSelectItems,
            });
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int receiptId, ReceiptItemCreateViewModel viewModel)
        {
            if (viewModel.ReceiptId != null && !await _receiptService.ExistsAsync(viewModel.ReceiptId.Value))
            {
                // Having null receipt ID is a red flag, send the user back to receipt list.
                return RedirectToAction("Index", "Receipts", new { area = "ReceiptManagement " });
            }

            if (viewModel.ReceiptId != receiptId)
            {
                // Having mismatched receipt IDs is unacceptable.
                // Fix the receipt ID then re-render the view.

                viewModel.ReceiptId = receiptId;
                viewModel.Products = await _productService.GetSelectListAsync();
                viewModel.Taxes = await _taxService.GetSelectListAsync();
                viewModel.People = await _personService.GetSelectListAsync();

                return View(viewModel);
            }

            if (viewModel.TaxIds.Count != 0)
            {
                foreach (var taxId in viewModel.TaxIds)
                {
                    if (!await _taxService.ExistsAsync(taxId))
                    {
                        ModelState.AddModelError(
                           nameof(viewModel.TaxIds),
                           "One or more taxes do not exist.");

                        break;
                    }
                }
            }

            if (viewModel.PeopleIds.Count != 0)
            {
                foreach (var personId in viewModel.PeopleIds)
                {
                    if (!await _personService.ExistsAsync(personId))
                    {
                        ModelState.AddModelError(
                         nameof(viewModel.PeopleIds),
                         "One or more people do not exist.");

                        break;
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _receiptItemService.CreateAsync(viewModel);

            return RedirectToAction("Index", new { receiptId = receiptId });
        }

        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int receiptId, int id)
        {
            var deleted = await _receiptItemService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", new { receiptId = receiptId });
        }
    }
}
