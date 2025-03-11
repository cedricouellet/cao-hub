using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
{
    public class StoreCategoryService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<StoreCategoryListViewModel> GetListAsync()
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

            return new StoreCategoryListViewModel
            {
                StoreCategories = storeCategories
            };
        }

        public Task<StoreCategoryViewModel?> GetAsync(int id)
        {
            return _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive && 
                            x.Id == id)
                .Select(x => new StoreCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListItemsAsync()
        {
            return await _context.StoreCategories
               .AsNoTracking()
               .Where(x => x.IsActive)
               .OrderBy(x => x.Name)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = x.Name,
               })
               .ToListAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public Task<bool> NameExistsAsync(string name)
        {
            return _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .AnyAsync();
        }

        public async Task<StoreCategoryViewModel?> CreateAsync(StoreCategoryCreateViewModel viewModel)
        {
            var entity = (await _context.StoreCategories.AddAsync(new Models.StoreCategory
            {
                Name = viewModel.Name!.Trim().Humanize(LetterCasing.Sentence),
                IsActive = true,
            })).Entity;

            await _context.SaveChangesAsync();

            return await GetAsync(entity.Id);
        }

        public async Task<StoreCategoryViewModel?> DeleteAsync(int id)
        {
            var storeCategory = await _context.StoreCategories
                .Where(x => x.Id == id &&
                            x.IsActive)
                .SingleOrDefaultAsync();

            if (storeCategory == null)
            {
                return null;
            }

            storeCategory.IsActive = false;

            await _context.SaveChangesAsync();

            return new StoreCategoryViewModel
            {
                Id = storeCategory.Id,
                Name = storeCategory.Name,
            };
        }
    }
}
