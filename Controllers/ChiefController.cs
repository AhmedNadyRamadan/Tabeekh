using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabeekh.DTOs;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ChiefController : ControllerBase
    {
        private readonly TabeekhDBContext _context;
        public ChiefController(TabeekhDBContext context)
        {
            _context = context;
        }

        // Get all Chiefs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chief>>> GetAllChiefs( 
            [FromQuery] int pageNumber,
            [FromQuery] int limit,
            [FromQuery] string name = "")
        {
            if (pageNumber <= 0 || limit <= 0)
            {
                return BadRequest(new { message = "Page number and limit must be greater than zero." });
            }

            IQueryable<Chief> query = _context.Chiefs;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(m => m.Name.Contains(name));
            }

            var totalChiefs = await query.CountAsync();

            var chiefs = await query
                // .Select(c=>new{
                //     c.Id,
                //     c.Name,
                //     c.TotalRate,
                // })
                .Skip(limit * (pageNumber - 1))
                .Take(limit)
                .ToListAsync();

                var totalCount = totalChiefs;
            return Ok(new{totalCount,items = chiefs});
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
        [HttpGet("meals/{id}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByChiefId(Guid id)
        {
            var chief = await _context.Chiefs
                .Include(c => c.Meals)
                .FirstOrDefaultAsync(c => c.Id == id);


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
            return Ok("Chief deleted successfully");
        }

        // GET: api/Chief/top10
        [HttpGet("top10")]
        public async Task<ActionResult<IEnumerable<object>>> GetTop10Chiefs()
        {
            var topChiefs = await _context.Cust_Chief_Reviews
                .GroupBy(r => r.Chief_Id)
                .Select(g => new
                {
                    id = g.Key,
                    name = _context.Chiefs.FirstOrDefault(c => c.Id == g.Key).Name,
                    // TotalReviews = g.Count(),
                    rate = g.Average(r => r.Rate),
                    photo = _context.EndUsers.FirstOrDefault(u=>u.Id == g.Key).Photo
                })
                .OrderByDescending(g => g.rate)
                .Take(10)
                .ToListAsync();

            // // If less than 10 ratings found, return all available sorted by rating
            // if (topChiefs.Count < 10)
            // {
            //     var allRankedChiefs = await _context.Cust_Chief_Reviews
            //         .GroupBy(r => r.Chief_Id)
            //         .Select(g => new
            //         {
            //             id = g.Key,
            //             name = _context.Chiefs.FirstOrDefault(c => c.Id == g.Key).Name,
            //             photo = _context.EndUsers.FirstOrDefault(u=>u.Id == g.Key).Photo
            //             rate = g.Average(r => r.Rate)
            //         })
            //         .OrderByDescending(g => g.rate)
            //         .ToListAsync();

            //     return Ok(allRankedChiefs);
            // }

            return Ok(topChiefs);
        }


        // Add Meal to Chief
        [HttpPost("Meals/{chiefId}")]
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
        [HttpDelete("meals/{mealId}")]
        public async Task<IActionResult> DeleteMeal( Guid mealId)
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
        [HttpGet("Reviews/{chiefId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetChiefReviews(Guid chiefId)
        {
            List<ReviewDTO> reviewsList = new List<ReviewDTO>();
            ReviewDTO review = new ReviewDTO();
            var Reviews = await _context.Cust_Chief_Reviews.Where(r=>r.Chief_Id == chiefId).ToListAsync();
            
            foreach (var rev in Reviews)
            {
                var customer = _context.Customers.FirstOrDefault(c=>c.Id == rev.Customer_Id);
                review.CustomerName = customer.Name;
                review.Comment = rev.Comment;
                review.Rate = rev.Rate;
                reviewsList.Add(review);
                review = new ReviewDTO();
            }
            return Ok(reviewsList);
        }
        [HttpGet("Rate/{chiefId}")]
        public async Task<IActionResult> GetChiefRate(Guid chiefId)
        {
            var Rates = await _context.Cust_Chief_Reviews.Where(r=>r.Chief_Id == chiefId).Select(r=>r.Rate).ToListAsync();
            float sum = 0;
            float AvgRate = 0;

            foreach (var rate in Rates)
            {
                sum +=rate;
            }
            AvgRate = sum/ Rates.Count;
            return Ok(AvgRate);
        }



    }
}