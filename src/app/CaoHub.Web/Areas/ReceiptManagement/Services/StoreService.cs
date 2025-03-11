using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
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
                 Name = x.Name,
                 StoreCategoryName = x.StoreCategory.Name,
             })
             .ToListAsync();

            return new StoreListViewModel
            {
                Stores = stores
            };
        }

        public Task<StoreViewModel?> GetAsync(int id)
        {
            return _context.Stores
                .AsNoTracking()
                .Include(x => x.StoreCategory)
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StoreCategoryName = x.StoreCategory.Name,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListItemsAsync(string? query = null)
        {
            return await _context.Stores
               .AsNoTracking()
               .Where(x => x.IsActive)
               .Where(x => string.IsNullOrWhiteSpace(query) || x.Name.ToLower().StartsWith(query.Trim().ToLower()))
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
            return _context.Stores
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public Task<bool> NameExistsAsync(string name)
        {
            return _context.Stores
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .AnyAsync();
        }

        public async Task<StoreViewModel?> CreateAsync(StoreCreateViewModel viewModel)
        {
            var entity = (await _context.Stores.AddAsync(new Models.Store
            {
                Name = viewModel.Name!.Trim().Humanize(LetterCasing.Sentence),
                IsActive = true,
                StoreCategoryId = viewModel.StoreCategoryId!.Value,
            })).Entity;

            await _context.SaveChangesAsync();

            return await GetAsync(entity.Id);
        }

        public async Task<StoreViewModel?> DeleteAsync(int id)
        {
            var store = await _context.Stores
                .Include(x => x.StoreCategory)
                .Where(x => x.Id == id && 
                            x.IsActive)
                .SingleOrDefaultAsync();

            if (store == null)
            {
                return null;
            }

            store.IsActive = false;

            await _context.SaveChangesAsync();

            return new StoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                StoreCategoryName = store.StoreCategory.Name,
            };
        }
    }
}
