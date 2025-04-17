using CaoHub.Data;
using CaoHub.Web.Areas.Facturio.ViewModels.Receipts;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.Facturio.Services
{
    public class ReceiptService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<ReceiptListViewModel> GetListAsync()
        {
            var receipts = await _context.Receipts
                .AsNoTracking()
                .Include(x => x.ReceiptDetails)
                .ThenInclude(x => x.ReceiptDetailsTaxes)
                .ThenInclude(x => x.Tax)
                .Include(x => x.PaidByPerson)
                .Include(x => x.Store)
                .Where(x => x.IsActive)
                .OrderBy(x => x.Date)
                .Select(x => new ReceiptListItemViewModel
                {
                    Id = x.Id,
                    PaidByPersonName = x.PaidByPerson.Name,
                    StoreName = x.Store.Name,
                    Date = x.Date.ToLocalTime(),
                    DiscountAmount = x.DiscountAmount,
                    PrepaidAmount = x.PrepaidAmount,
                    Total = x.Total,
                })
                .ToListAsync();
            
            return new ReceiptListViewModel
            {
                Receipts = receipts,
            };
        }
    }
}
