using CaoHub.Api.Areas.ReceiptManagement.Models;
using CaoHub.Api.Areas.ReceiptManagement.Requests;
using CaoHub.Api.Areas.ReceiptManagement.Responses;
using CaoHub.Api.Configuration;
using CaoHub.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CaoHub.Api.Areas.ReceiptManagement.Controllers
{
    [Area("ReceiptManagement")]
    [ApiController]
    [Route("api/store-categories")]
    public class StoreCategoriesController(CaoHubDbContext context, IOptions<PaginationSettings> paginationOptions) : ControllerBase
    {
        private readonly CaoHubDbContext _context = context;

        private readonly PaginationSettings paginationSettings = paginationOptions.Value;

        [HttpGet]
        public async Task<ActionResult<StoreCategoryListResponse>> Get([FromQuery] StoreCategoryListRequest request)
        {
            request.Limit ??= paginationSettings.DefaultLimit;

            var storeCategories = _context.StoreCategories
                .AsNoTracking()
                .Where(x => x.IsActive);

            var filteredStoreCategories = await storeCategories
                .Skip(request.Offset ?? 0)
                .Take(request.Limit.Value)
                .OrderBy(x => x.Name)
                .Select(x => new StoreCategoryListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return Ok(new StoreCategoryListResponse
            {
                Items = filteredStoreCategories,
                Limit = request.Limit.Value,
                Offset = request.Offset ?? 0,
                TotalCount = storeCategories.Count()
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreCategoryDetailsResponse>> Get(int id)
        {
            var storeCategory = await _context.StoreCategories
                .SingleOrDefaultAsync(x => x.Id == id);

            if (storeCategory == null)
            {
                return NotFound();
            }

            return Ok(new StoreCategoryDetailsResponse
            {
                Id = storeCategory.Id,
                Name = storeCategory.Name,
            });
        }

        [HttpPost]
        public async Task<ActionResult<StoreCategoryCreateUpdateResponse>> Post(StoreCategoryCreateRequest request)
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

            return Created(Url.Action(nameof(Get), new { id = entity.Id }), new StoreCategoryCreateUpdateResponse
            {
                Id = entity.Id,
                Name = entity.Name,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StoreCategoryCreateUpdateResponse>> Put(int id, StoreCategoryUpdateRequest request)
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

            return Ok(new StoreCategoryCreateUpdateResponse
            {
                Id = entity.Id,
                Name = entity.Name,
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
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

            return Ok();
        }
    }
}
