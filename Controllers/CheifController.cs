using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class CheifController : ControllerBase
    {
        private readonly TabeekhDBContext _context;
        public CheifController(TabeekhDBContext context)
        {
            _context = context;
        }

        // Get all Chiefs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chief>>> GetAllChiefs( 
            [FromQuery] int pageNumber,
            [FromQuery] int limit,
            [FromQuery] string nameFilter = "")
        {
            if (pageNumber <= 0 || limit <= 0)
            {
                return BadRequest(new { message = "Page number and limit must be greater than zero." });
            }

            IQueryable<Chief> query = _context.Chiefs;

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(m => m.Name.Contains(nameFilter));
            }

            var totalChiefs = await query.CountAsync();

            var chiefs = await query
                .Skip(limit * (pageNumber - 1))
                .Take(limit)
                .ToListAsync();

            var paginationMetadata = new
            {
                totalCount = totalChiefs,
                pageNumber,
                pageSize = limit,
                totalPages = (int)Math.Ceiling(totalChiefs / (double)limit)
            };

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(chiefs);
        }

        // Get Chief by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Chief>> GetChiefById(Guid id)
        {
            var chief = await _context.Chiefs.FindAsync(id);
            //if (chief == null)
            //{
            //    return NotFound("Chief not found.");
            //}
            return Ok(chief);
        }

        // Get Chief by Name
        [HttpGet("{name:alpha}")]
        public async Task<ActionResult<IEnumerable<Chief>>> GetChiefByName(string name)
        {
            var chiefs = await _context.Chiefs
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            //if (!chiefs.Any())
            //{
            //    return NotFound("No chiefs found.");
            //}

            return Ok(chiefs);
        }

        // Get Meals by Chief ID
        [HttpGet("{id}/meals")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByChiefId(Guid id)
        {
            var chief = await _context.Chiefs
                .Include(c => c.Meals)
                .FirstOrDefaultAsync(c => c.Id == id);

            //if (chief == null)
            //{
            //    return NotFound("Chief not found.");
            //}

            return Ok(chief.Meals);
        }

        // Add a new Chief
        [HttpPost]
        public async Task<ActionResult<Chief>> AddChief([FromBody] Chief chief)
        {
            if (chief == null)
            {
                return BadRequest("Chief data is null.");
            }

            _context.Chiefs.Add(chief);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChiefById), new { id = chief.Id }, chief); //return 201 Created 
        }

        // Update Chief
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChief(Guid id, [FromBody] Chief chief)
        {
            if (chief == null)
            {
                return BadRequest("Chief data is null.");
            }

            var existingChief = await _context.Chiefs.FindAsync(id);
            if (existingChief == null)
            {
                return NotFound("Chief not found.");
            }

            existingChief.Name = chief.Name;
            existingChief.Email = chief.Email;
            existingChief.Phone = chief.Phone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete Chief
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChief(Guid id)
        {
            var chief = await _context.Chiefs.FindAsync(id);
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }

            _context.Chiefs.Remove(chief);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Chiefs/top10
        [HttpGet("top10")]
        public async Task<ActionResult<IEnumerable<object>>> GetTop10Chiefs()
        {
            var topChiefs = await _context.Cust_Chief_Reviews
                .GroupBy(r => r.Chief_Id)
                .Select(g => new
                {
                    ChiefId = g.Key,
                    GetChiefByName = _context.Chiefs.FirstOrDefault(c => c.Id == g.Key).Name,
                    TotalReviews = g.Count(),
                    AverageRating = g.Average(r => r.Rate)
                })
                .OrderByDescending(g => g.AverageRating)
                .Take(10)
                .ToListAsync();

            // If less than 10 ratings found, return all available sorted by rating
            if (topChiefs.Count < 10)
            {
                var allRankedChiefs = await _context.Cust_Chief_Reviews
                    .GroupBy(r => r.Chief_Id)
                    .Select(g => new
                    {
                        ChiefId = g.Key,
                        AverageRating = g.Average(r => r.Rate)
                    })
                    .OrderByDescending(g => g.AverageRating)
                    .ToListAsync();

                return Ok(allRankedChiefs);
            }

            return Ok(topChiefs);
        }


        // Add Meal to Chief
        [HttpPost("{chiefId}/meals")]
        public async Task<ActionResult<Meal>> AddMealToChief(Guid chiefId, [FromBody] Meal meal)
        {
            if (meal == null)
            {
                return BadRequest("Meal data is null.");
            }

            var chief = await _context.Chiefs.FindAsync(chiefId);
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }

            meal.Chief_Id = chiefId;
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMealsByChiefId), new { id = chiefId }, meal);
        }

        // Update Meal
        [HttpPut("{chiefId}/meals/{mealId}")]
        public async Task<IActionResult> UpdateMeal(Guid chiefId, Guid mealId, [FromBody] Meal meal)
        {
            if (meal == null)
            {
                return BadRequest("Meal data is null.");
            }

            var existingMeal = await _context.Meals.FindAsync(mealId);
            if (existingMeal == null)
            {
                return NotFound("Meal not found.");
            }

            existingMeal.Name = meal.Name;
            existingMeal.Photo = meal.Photo;
            existingMeal.Prepration_Time = meal.Prepration_Time;
            existingMeal.Price = meal.Price;
            existingMeal.Available = meal.Available;
            existingMeal.Ingredients = meal.Ingredients;
            existingMeal.Recipe = meal.Recipe;
            existingMeal.Measure_unit = meal.Measure_unit;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete Meal
        [HttpDelete("{chiefId}/meals/{mealId}")]
        public async Task<IActionResult> DeleteMeal(Guid chiefId, Guid mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null)
            {
                return NotFound("Meal not found.");
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}