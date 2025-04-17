using CaoHub.Data;
using CaoHub.Data.Extensions;
using CaoHub.Data.Models.Facturio;
using CaoHub.Web.Areas.Facturio.ViewModels.Stores;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.Facturio.Services
{
    public class StoreService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<StoreListViewModel> GetListAsync()
        {
            var stores = await _context.Stores
                .AsNoTracking()
                .Include(x => x.StoreCategory)
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    StoreCategoryId = x.StoreCategoryId,
                    Name = x.Name,
                    StoreCategoryName = x.StoreCategory.Name,
                })
                .ToListAsync();

            return new StoreListViewModel
            {
                Stores = stores,
            };
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            return await _context.Stores
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

        public async Task<StoreViewModel?> GetAsync(int id)
        {
            return await _context.Stores
                .AsNoTracking()
                .Include(x => x.StoreCategory)
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    StoreCategoryId = x.StoreCategoryId,
                    Name = x.Name,
                    StoreCategoryName = x.StoreCategory.Name,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Stores
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public async Task<bool> ExistsAsync(string name, int? excludeId = null)
        {
            return await _context.Stores
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .WhereIf(excludeId != null, x => x.Id != excludeId)
                .AnyAsync();
        }

        public async Task<int> CreateOrUpdateAsync(StoreEditViewModel viewModel)
        {
            var entity = _context.Stores.Update(new Store
            {
                Id = viewModel.Id ?? 0,
                StoreCategoryId = viewModel.StoreCategoryId ?? 0,
                Name = viewModel.Name!.Trim(),
                IsActive = true,
            }).Entity;

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeactivateAsync(int id)
        {
            var entity = await _context.Stores
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
