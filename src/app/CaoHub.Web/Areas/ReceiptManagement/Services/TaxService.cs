using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Taxes;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
{
    public class TaxService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<TaxListViewModel> GetListAsync()
        {
            var taxes = await _context.Taxes
             .AsNoTracking()
             .Where(x => x.IsActive)
             .OrderBy(x => x.Name)
             .ThenByDescending(x => x.Rate)
             .Select(x => new TaxViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 Description = x.Description,
                 Rate = x.Rate,
             })
             .ToListAsync();

            return new TaxListViewModel
            {
                Taxes = taxes,
            };
        }

        public Task<TaxViewModel?> GetAsync(int id)
        {
            return _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new TaxViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Rate = x.Rate,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            return await _context.Taxes
               .AsNoTracking()
               .Where(x => x.IsActive)
               .OrderBy(x => x.Name)
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = $"{x.Name} ({x.Rate:P3})",
               })
               .ToListAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public Task<bool> NameExistsAsync(string name)
        {
            return _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .AnyAsync();
        }

        public async Task<TaxViewModel?> CreateAsync(TaxCreateViewModel viewModel)
        {
            var entity = (await _context.Taxes.AddAsync(new Models.Tax
            {
                Name = viewModel.Name!.Trim().Humanize(LetterCasing.Sentence),
                Description = viewModel.Description?.Trim(),
                Rate = viewModel.RatePercentage!.Value * 0.01M,
                IsActive = true,
            })).Entity;

            await _context.SaveChangesAsync();

            return await GetAsync(entity.Id);
        }

        public async Task<TaxViewModel?> DeleteAsync(int id)
        {
            var tax = await _context.Taxes
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (tax == null)
            {
                return null;
            }

            tax.IsActive = false;

            await _context.SaveChangesAsync();

            return new TaxViewModel
            {
                Id = tax.Id,
                Name = tax.Name,
                Description = tax.Description,
                Rate = tax.Rate,
            };
        }
    }
}
