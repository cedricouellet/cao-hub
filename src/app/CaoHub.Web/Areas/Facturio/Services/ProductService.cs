using CaoHub.Data;
using CaoHub.Data.Extensions;
using CaoHub.Data.Models.Facturio;
using CaoHub.Web.Areas.Facturio.ViewModels.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.Facturio.Services
{
    public class ProductService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<ProductListViewModel> GetListAsync()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return new ProductListViewModel
            {
                Products = products,
            };
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            return await _context.Products
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

        public async Task<ProductViewModel?> GetAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public async Task<bool> ExistsAsync(string name, int? excludeId = null)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .WhereIf(excludeId != null, x => x.Id != excludeId)
                .AnyAsync();
        }

        public async Task<int> CreateOrUpdateAsync(ProductEditViewModel viewModel)
        {
            var entity = _context.Products.Update(new Product
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
            var entity = await _context.Products
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
