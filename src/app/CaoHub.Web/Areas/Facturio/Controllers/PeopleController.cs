using CaoHub.Web.Areas.Facturio.Services;
using CaoHub.Web.Areas.Facturio.ViewModels.People;
using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CaoHub.Web.Areas.Facturio.Controllers
{
    [Area("Facturio")]
    public class PeopleController(
        PersonService personService,
        IStringLocalizer<SharedResource> localizer) : Controller
    {
        private readonly PersonService _personService = personService;

        private readonly IStringLocalizer _localizer = localizer;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _personService.GetListAsync();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PersonEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);
        
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _personService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personService.GetAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(new PersonEditViewModel
            {
                Id = person.Id,
                Name = person.Name,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonEditViewModel viewModel)
        {
            await ValidateAsync(viewModel);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _personService.CreateOrUpdateAsync(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _personService.GetAsync(id);

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
            await _personService.DeactivateAsync(id);

            return RedirectToAction("Index");
        }

        private async Task ValidateAsync(PersonEditViewModel viewModel)
        {
            if (viewModel.Name != null &&
                await _personService.ExistsAsync(viewModel.Name, viewModel.Id))
            {
                ModelState.AddModelError(
                    nameof(viewModel.Name),
                    _localizer["Facturio_People_Name_ErrorExists"]);
            }
        }
    }
}
