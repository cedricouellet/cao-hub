using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Receipts;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [Route("[area]/[controller]")]
    public class ReceiptsController(
        ReceiptService receiptService, 
        PersonService personService, 
        StoreService storeService) : Controller
    {
        private readonly ReceiptService _receiptService = receiptService;

        private readonly PersonService _personService = personService;

        private readonly StoreService _storeService = storeService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _receiptService.GetListAsync();

            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var peopleSelectList = await _personService.GetSelectListAsync();
            var storesSelectList = await _storeService.GetSelectListAsync();

            return View(new ReceiptCreateViewModel
            {
                People = peopleSelectList,
                Stores = storesSelectList,
            });
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptCreateViewModel viewModel)
        {
            if (viewModel.PaidByPersonId != null && 
                !await _personService.ExistsAsync(viewModel.PaidByPersonId.Value))
            {
                ModelState.AddModelError(
                    nameof(viewModel.PaidByPersonId), 
                    "This person does not exist.");
            }

            if (viewModel.StoreId != null && 
                !await _storeService.ExistsAsync(viewModel.StoreId.Value))
            {
                ModelState.AddModelError(
                    nameof(viewModel.StoreId),
                    "This store does not exist.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var created = await _receiptService.CreateAsync(viewModel);

            return RedirectToAction("Index", "ReceiptItems", new { area = "ReceiptManagement", receiptId = created!.Id });
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _receiptService.GetAsync(id);

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
            var deleted = await _receiptService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
