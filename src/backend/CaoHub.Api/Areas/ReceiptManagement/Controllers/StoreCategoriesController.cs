using CaoHub.Api.Areas.ReceiptManagement.Models;
using CaoHub.Api.Areas.ReceiptManagement.Requests.StoreCategories;
using CaoHub.Api.Areas.ReceiptManagement.Responses.StoreCategories;
using CaoHub.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Api.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [ApiController]
    [Route("api/receipt-management/store-categories")]
    public class StoreCategoriesController(CaoHubDbContext context) : ControllerBase
    {
        private readonly CaoHubDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreCategoryDto>>> Get()
        {
            var storeCategories = await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new StoreCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return Ok(storeCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreCategoryDto>> Get(int id)
        {
            var storeCategory = await _context.StoreCategories
                .Where(x => x.Id == id)
                .Select(x => new StoreCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .SingleOrDefaultAsync();

            if (storeCategory == null)
            {
                return NotFound();
            }

            return Ok(storeCategory);
        }

        [HttpPost]
        public async Task<ActionResult<StoreCategoryDto>> Post(StoreCategoryCreateParams request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            request.Name = request.Name!.Trim();

            if (await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A store category with this name already exists");
                return ValidationProblem(ModelState);
            }

            var entity = (await _context.StoreCategories.AddAsync(new StoreCategory
            {
                Name = request.Name,
            }))?.Entity;

            if (entity == null)
            {
                return Problem();
            }

            await _context.SaveChangesAsync();

            return Created(Url.Action(nameof(Get), new { id = entity.Id }), new StoreCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StoreCategoryDto>> Put(int id, StoreCategoryUpdateParams request)
        {
            if (id != request.Id)
            {
                return Conflict();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var entity = await _context.StoreCategories.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            request.Name = request.Name!.Trim();

            if (await _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive && x.Id != id)
                .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A store category with this name already exists");
                return ValidationProblem(ModelState);
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync();

            return Ok(new StoreCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreCategoryDto>> Delete(int id)
        {
            var entity = await _context.StoreCategories
                .Where(x => x.IsActive)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.IsActive = false;

            await _context.SaveChangesAsync();

            return Ok(new StoreCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            });
        }
    }
}
