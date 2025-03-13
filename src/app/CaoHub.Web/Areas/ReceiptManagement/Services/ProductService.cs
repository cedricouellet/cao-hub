using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Products;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
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
                Products = products
            };
        }

        public async Task<int?> GetIdAsync(string name)
        {
            return (await _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .FirstOrDefaultAsync())
                ?.Id;
        }

        public Task<ProductViewModel?> GetAsync(int id)
        {
            return _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .SingleOrDefaultAsync();
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

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public Task<bool> NameExistsAsync(string name)
        {
            return _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .AnyAsync();
        }

        public async Task<ProductViewModel?> CreateAsync(ProductCreateViewModel viewModel)
        {
            var entity = (await _context.Products.AddAsync(new Models.Product
            {
                Name = viewModel.Name!.Trim().Humanize(LetterCasing.Sentence),
                IsActive = true,
            })).Entity;

            await _context.SaveChangesAsync();

            return await GetAsync(entity.Id);
        }

        public async Task<ProductViewModel?> DeleteAsync(int id)
        {
            var product = await _context.Products
                .Where(x => x.Id == id &&
                            x.IsActive)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return null;
            }

            product.IsActive = false;

            await _context.SaveChangesAsync();

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
            };
        }

    }
}
