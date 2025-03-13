using CaoHub.Web.Areas.ReceiptManagement.Services;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.People;
using Microsoft.AspNetCore.Mvc;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [Route("[area]/[controller]")]
    public class PeopleController(PersonService personService) : Controller
    {
        private readonly PersonService _personService = personService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _personService.GetListAsync();

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new PersonCreateViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateViewModel viewModel)
        {
            if (viewModel.Name != null &&
                await _personService.NameExistsAsync(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    "A person with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _personService.CreateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _personService.GetAsync(id);

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
            var deleted = await _personService.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
