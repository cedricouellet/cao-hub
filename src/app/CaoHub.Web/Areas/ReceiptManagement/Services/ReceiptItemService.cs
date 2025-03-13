using CaoHub.Web.Areas.ReceiptManagement.Models;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels.ReceiptItems;
using CaoHub.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
{
    public class ReceiptItemService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<ReceiptItemListViewModel> GetListAsync(int receiptId)
        {
            var receipt = await _context.Receipts
                .AsNoTracking()
                .Include(x => x.PaidByPerson)
                .Include(x => x.Store)
                .Where(x => x.IsActive && 
                            x.Id == receiptId)
                .SingleAsync();

            var receiptItems = await _context.ReceiptItems
                .AsNoTracking()
                .Include(x => x.ReceiptItemsPeople)
                .ThenInclude(x => x.Person)
                .Include(x => x.ReceiptItemsTaxes)
                .ThenInclude(x => x.Tax)
                .Include(x => x.Product)
                .Where(x => x.IsActive &&
                            x.ReceiptId == receiptId)
                .OrderBy(x => x.Id)
                .Select(x => new ReceiptItemViewModel
                {
                    Id = x.Id,
                    ReceiptId = x.ReceiptId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    UnitDiscount = x.UnitDiscount,
                    ProductName = x.Product.Name,
                    PeopleNames = x.ReceiptItemsPeople.Select(rip => rip.Person.Name),
                    TaxNames = x.ReceiptItemsTaxes.Select(rit => rit.Tax.Name),
                })
                .ToListAsync();

            return new ReceiptItemListViewModel
            {
                ReceiptId = receiptId,
                ReceiptDate = receipt.Date.ToLocalTime(),
                ReceiptPaidByPersonName = receipt.PaidByPerson.Name,
                ReceiptStoreName = receipt.Store.Name,
                ReceiptItems = receiptItems,
            };
        }

        public Task<ReceiptItemViewModel?> GetAsync(int id)
        {
            return _context.ReceiptItems
               .AsNoTracking()
               .Include(x => x.ReceiptItemsPeople)
               .ThenInclude(x => x.Person)
               .Include(x => x.ReceiptItemsTaxes)
               .ThenInclude(x => x.Tax)
               .Include(x => x.Product)
               .Where(x => x.IsActive &&
                           x.Id == id)
               .Select(x => new ReceiptItemViewModel
               {
                   Id = x.Id,
                   ReceiptId = x.ReceiptId,
                   Quantity = x.Quantity,
                   UnitPrice = x.UnitPrice,
                   UnitDiscount = x.UnitDiscount,
                   ProductName = x.Product.Name,
                   PeopleNames = x.ReceiptItemsPeople.Select(rip => rip.Person.Name),
                   TaxNames = x.ReceiptItemsTaxes.Select(rit => rit.Tax.Name),
               })
               .SingleOrDefaultAsync();
        }

        public async Task<ReceiptItemViewModel?> CreateAsync(ReceiptItemCreateViewModel viewModel)
        {
            int? productId = (await _context.Products
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == viewModel.ProductName!.Trim().ToLower())
                .FirstOrDefaultAsync())?.Id;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (productId == null)
                {
                    var product = (await _context.Products.AddAsync(new Product
                    {
                        Name = viewModel.ProductName!.Trim(),
                        IsActive = true,
                    })).Entity;

                    await _context.SaveChangesAsync();

                    productId = product.Id;
                }

                var entity = (await _context.ReceiptItems.AddAsync(new ReceiptItem
                {
                    ReceiptId = viewModel.ReceiptId!.Value,
                    ProductId = productId.Value,
                    Quantity = viewModel.Quantity,
                    UnitPrice = viewModel.UnitPrice!.Value,
                    UnitDiscount = viewModel.UnitDiscount,
                    IsActive = true,
                })).Entity;

                _context.SaveChanges();

                if (viewModel.TaxIds.Count != 0)
                {
                    _context.ReceiptItemsTaxes.AddRange(viewModel.TaxIds.Select(taxId => new ReceiptItemTax
                    {
                        ReceiptItemId = entity.Id,
                        TaxId = taxId,
                    }));
                }

                if (viewModel.PeopleIds.Count != 0)
                {
                    _context.ReceiptItemsPeople.AddRange(viewModel.PeopleIds.Select(personId => new ReceiptItemPerson
                    {
                        ReceiptItemId = entity.Id,
                        PersonId = personId
                    }));
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return await GetAsync(entity.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ReceiptItemViewModel?> DeleteAsync(int id)
        {
            var receiptItem = await _context.ReceiptItems
                .Include(x => x.ReceiptItemsPeople)
               .ThenInclude(x => x.Person)
               .Include(x => x.ReceiptItemsTaxes)
               .ThenInclude(x => x.Tax)
               .Include(x => x.Product)
               .Where(x => x.IsActive &&
                           x.Id == id)
               .SingleOrDefaultAsync();

            if (receiptItem == null)
            {
                return null;
            }

            receiptItem.IsActive = false;

            await _context.SaveChangesAsync();

            return new ReceiptItemViewModel
            {
                Id = receiptItem.Id,
                ReceiptId = receiptItem.ReceiptId,
                Quantity = receiptItem.Quantity,
                UnitPrice = receiptItem.UnitPrice,
                UnitDiscount = receiptItem.UnitDiscount,
                ProductName = receiptItem.Product.Name,
                PeopleNames = receiptItem.ReceiptItemsPeople.Select(rip => rip.Person.Name),
                TaxNames = receiptItem.ReceiptItemsTaxes.Select(rit => rit.Tax.Name),
            };
        }
    }
}
