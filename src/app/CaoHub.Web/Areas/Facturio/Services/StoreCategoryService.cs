using CaoHub.Data;
using CaoHub.Data.Extensions;
using CaoHub.Data.Models.Facturio;
using CaoHub.Web.Areas.Facturio.ViewModels.StoreCategories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.Facturio.Services
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
                StoreCategories = storeCategories,
            };
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
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

        public async Task<StoreCategoryViewModel?> GetAsync(int id)
        {
            return await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new StoreCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive && 
                            x.Id == id)
                .AnyAsync();
        }

        public async Task<bool> ExistsAsync(string name, int? excludeId = null)
        {
            return await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive && 
                            x.Name.ToLower() == name.Trim().ToLower())
                .WhereIf(excludeId != null, x => x.Id != excludeId)
                .AnyAsync();
        }

        public async Task<int> CreateOrUpdateAsync(StoreCategoryEditViewModel viewModel)
        {
            var entity = _context.StoreCategories.Update(new StoreCategory
            {
                Id = viewModel.Id ?? 0,
                Name = viewModel.Name!.Trim(),
                IsActive = true,
            }).Entity;

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeactivateAsync(int id)
        {
            var entity = await _context.StoreCategories
                .Where(x => x.IsActive && 
                            x.Id == id)
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                return;
            }

            entity.IsActive = false;

            await _context.SaveChangesAsync();
        }
    }
}
