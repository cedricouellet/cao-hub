using CaoHub.Web.Areas.ReceiptManagement.Controllers;
using CaoHub.Web.Areas.ReceiptManagement.ViewModels;
using CaoHub.Web.Data;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Web.Areas.ReceiptManagement.Services
{
    public class PersonService(CaoHubDbContext context)
    {
        private readonly CaoHubDbContext _context = context;

        public async Task<PersonListViewModel> GetListAsync()
        {
            var people = await _context.People
             .AsNoTracking()
             .Where(x => x.IsActive)
             .OrderBy(x => x.Name)
             .Select(x => new PersonViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
             })
             .ToListAsync();

            return new PersonListViewModel
            {
                People = people
            };
        }

        public Task<PersonViewModel?> GetAsync(int id)
        {
            return _context.People
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .Select(x => new PersonViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListItemsAsync(string? query = null)
        {
            return await _context.People
               .AsNoTracking()
               .Where(x => x.IsActive)
               .Where(x => string.IsNullOrWhiteSpace(query) || 
                           x.Name.ToLower().StartsWith(query.Trim().ToLower()))
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
            return _context.People
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Id == id)
                .AnyAsync();
        }

        public Task<bool> NameExistsAsync(string name)
        {
            return _context.People
                .AsNoTracking()
                .Where(x => x.IsActive &&
                            x.Name.ToLower() == name.Trim().ToLower())
                .AnyAsync();
        }

        public async Task<PersonViewModel?> CreateAsync(PersonCreateViewModel viewModel)
        {
            var entity = (await _context.People.AddAsync(new Models.Person
            {
                Name = viewModel.Name!.Trim(),
                IsActive = true,
            })).Entity;

            await _context.SaveChangesAsync();

            return await GetAsync(entity.Id);
        }

        public async Task<PersonViewModel?> DeleteAsync(int id)
        {
            var person = await _context.People
                .Where(x => x.Id == id &&
                            x.IsActive)
                .SingleOrDefaultAsync();

            if (person == null)
            {
                return null;
            }

            person.IsActive = false;

            await _context.SaveChangesAsync();

            return new PersonViewModel
            {
                Id = person.Id,
                Name = person.Name,
            };
        }
    }
}
