using CaoHub.Web.Areas.ReceiptManagement.ViewModels.Receipts;
using CaoHub.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
{
    public class ReceiptService(CaoHubDbContext context) 
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<ReceiptListViewModel> GetListAsync()
        {
            var receipts = await _context.Receipts
                .AsNoTracking()
                .Include(x => x.PaidByPerson)
                .Include(x => x.Store)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Date)
                .ThenBy(x => x.Store.Name)
                .Select(x => new ReceiptViewModel
                {
                    Id = x.Id,
                    PaidByPersonName = x.PaidByPerson.Name,
                    StoreName = x.Store.Name,
                    Date = x.Date.ToLocalTime(),
                })
                .ToListAsync();

            return new ReceiptListViewModel
            {
                Receipts = receipts,
            };
        }

        public Task<ReceiptViewModel?> GetAsync(int id)
        {
            return _context.Receipts
                .AsNoTracking()
                .Include(x => x.PaidByPerson)
                .Include(x => x.Store)
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new ReceiptViewModel
                {
                    Id = x.Id,
                    PaidByPersonName = x.PaidByPerson.Name,
                    StoreName = x.Store.Name,
                    Date = x.Date.ToLocalTime(),
                })
                .SingleOrDefaultAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Receipts
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public async Task<ReceiptViewModel?> CreateAsync(ReceiptCreateViewModel viewModel)
        {
            var entity = (await _context.Receipts.AddAsync(new Models.Receipt
            {
                Date = viewModel.Date!.Value.ToUniversalTime(),
                PaidByPersonId = viewModel.PaidByPersonId!.Value,
                StoreId = viewModel.StoreId!.Value,
                IsActive = true,
            })).Entity;

            await _context.SaveChangesAsync();

            return await GetAsync(entity.Id);
        }

        public async Task<ReceiptViewModel?> DeleteAsync(int id)
        {
            var receipt = await _context.Receipts
                .Include(x => x.PaidByPerson)
                .Include(x => x.Store)
                .Where(x => x.IsActive &&
                            x.Id == id)
                .SingleOrDefaultAsync();

            if (receipt == null)
            {
                return null;
            }

            receipt.IsActive = false;

            await _context.SaveChangesAsync();

            return new ReceiptViewModel
            {
                Id = receipt.Id,
                PaidByPersonName = receipt.PaidByPerson.Name,
                StoreName = receipt.Store.Name,
                Date = receipt.Date.ToLocalTime(),
            };
        }
    }
}
