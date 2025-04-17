using CaoHub.Data;
using CaoHub.Data.Extensions;
using CaoHub.Data.Models.Facturio;
using CaoHub.Web.Areas.Facturio.ViewModels.Taxes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace CaoHub.Web.Areas.Facturio.Services
{
    public class TaxService(
        CaoHubDbContext context,
        CalculationMethodService calculationMethodService)
    {
        private readonly CaoHubDbContext _context = context;

        private readonly CalculationMethodService _calculationMethodService = calculationMethodService;

        public async Task<TaxListViewModel> GetListAsync()
        {
            var taxes = await _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.CalculationMethod)
                .ThenBy(x => x.Value)
                .Select(x => new TaxViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.CalculationMethod == CalculationMethod.PercentageBased 
                        ? x.Value * 100m 
                        : x.CalculationMethod == CalculationMethod.AdditiveValue 
                            ? x.Value 
                            : x.Value,
                    CalculationMethod = x.CalculationMethod,
                    FormattedValue = x.FormattedValue,
                })
                .ToListAsync();

            return new TaxListViewModel
            {
                Taxes = taxes,
            };
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            return await _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.CalculationMethod)
                .ThenBy(x => x.Value)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Name} ({x.FormattedValue})",
                })
                .ToListAsync();
        }

        public async Task<TaxViewModel?> GetAsync(int id)
        {
            return await _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new TaxViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.CalculationMethod == CalculationMethod.PercentageBased
                        ? x.Value * 100m
                        : x.CalculationMethod == CalculationMethod.AdditiveValue
                            ? x.Value
                            : x.Value,
                    CalculationMethod = x.CalculationMethod,
                    FormattedValue = x.FormattedValue,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public async Task<bool> ExistsAsync(string name, int? excludeId = null)
        {
            return await _context.Taxes
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .WhereIf(excludeId != null, x => x.Id != excludeId)
                .AnyAsync();
        }

        public async Task<int> CreateOrUpdateAsync(TaxEditViewModel viewModel)
        {
            var entity = _context.Taxes.Update(new Tax
            {
                Id = viewModel.Id ?? 0,
                Name = viewModel.Name!.Trim(),
                Description = viewModel.Description?.Trim(),
                Value = _calculationMethodService.CalculateValue(viewModel.Value!.Value, viewModel.CalculationMethod!.Value),
                CalculationMethod = viewModel.CalculationMethod!.Value,
                IsActive = true
            }).Entity;

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeactivateAsync(int id)
        {
            var entity = await _context.Taxes
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
