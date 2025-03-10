using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    public class StoreCategoriesController(CaoHubDbContext context) : Controller
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            var storeCategories = await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new StoreCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return View(new StoreCategoryListViewModel
            {
                StoreCategories = storeCategories,
            });
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
            if (viewModel.Name != null && await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive && 
                            x.Name.ToLower() == viewModel.Name.Trim().ToLower())
                .AnyAsync())
            {
                ModelState.AddModelError("Name", "A store category with this name already exists");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _context.StoreCategories.AddAsync(new Models.StoreCategory
            {
                Name = viewModel.Name!.Trim().Humanize(LetterCasing.Title),
                IsActive = true,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var storeCategory = await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new StoreCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .SingleOrDefaultAsync();

            if (storeCategory == null)
            {
                return NotFound();
            }

            return View(storeCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var storeCategory = await _context.StoreCategories
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (storeCategory == null)
            {
                return NotFound();
            }

            storeCategory.IsActive = false;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
