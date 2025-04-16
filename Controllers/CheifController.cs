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
        public IActionResult GetAllChiefs()
        {
            var chiefs = _context.Chiefs.ToList();
            if (chiefs == null || !chiefs.Any())
            {
                return NotFound("No chiefs found.");
            }
            return Ok(chiefs);
        }
        // Get Chief by ID
        [HttpGet("{id}")]
        public IActionResult GetChiefs()
        {
            var chiefs = _context.Chiefs.ToList();
            if (chiefs == null || !chiefs.Any())
            {
                return NotFound("No chiefs found.");
            }
            return Ok(chiefs);
        }
        // Get Chief by Name
        [HttpGet("name/{name}")]
        public IActionResult GetChiefByName(string name)
        {
            var chief = _context.Chiefs.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }
            return Ok(chief);
        }
        // Get Meals by Chief ID
        [HttpGet("{id}/meals")]
        public IActionResult GetMealsByChiefId(Guid id)
        {
            var chief = _context.Chiefs.Include(c => c.Meals).FirstOrDefault(c => c.Id == id);
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }
            return Ok(chief.Meals);
        }
        // Add a new Chief
        [HttpPost]
        public IActionResult AddChief([FromBody] Chief chief)
        {
            if (chief == null)
            {
                return BadRequest("Chief data is null.");
            }
            _context.Chiefs.Add(chief);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllChiefs), new { id = chief.Id }, chief);
        }
        // Update Chief
        [HttpPut("{id}")]
        public IActionResult UpdateChief(Guid id, [FromBody] Chief chief)
        {
            if (chief == null)
            {
                return BadRequest("Chief data is null.");
            }
            var existingChief = _context.Chiefs.Find(id);
            if (existingChief == null)
            {
                return NotFound("Chief not found.");
            }
            existingChief.Name = chief.Name;
            existingChief.Email = chief.Email;
            existingChief.Phone = chief.Phone;
            _context.SaveChanges();
            return NoContent();
        }
        // Delete Chief
        [HttpDelete("{id}")]
        public IActionResult DeleteChief(Guid id)
        {
            var chief = _context.Chiefs.Find(id);
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }
            _context.Chiefs.Remove(chief);
            _context.SaveChanges();
            return NoContent();
        }
        //get top 10 chiefs ratings
        [HttpGet("top10")]
        public IActionResult GetTop10Chiefs()
        {
            var topChiefs = _context.Cust_Chief_Reviews
                .GroupBy(r => r.Chief_Id)
                .Select(g => new
                {
                    ChiefId = g.Key,
                    AverageRating = g.Average(r => r.Rate)
                })
                .OrderByDescending(g => g.AverageRating)
                .Take(10)
                .ToList();
            if (topChiefs == null || !topChiefs.Any())
            {
                return NotFound("No chiefs found.");
            }
            return Ok(topChiefs);
        }
        // Chief add meal
        [HttpPost("{chiefId}/meals")]
        public IActionResult AddMealToChief(Guid chiefId, [FromBody] Meal meal)
        {
            if (meal == null)
            {
                return BadRequest("Meal data is null.");
            }
            var chief = _context.Chiefs.Find(chiefId);
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }
            meal.Chief_Id = chiefId;
            _context.Meals.Add(meal);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMealsByChiefId), new { id = chiefId }, meal);
        }
        // Chief update meal
        [HttpPut("{chiefId}/meals/{mealId}")]
        public IActionResult UpdateMeal(Guid chiefId, Guid mealId, [FromBody] Meal meal)
        {
            if (meal == null)
            {
                return BadRequest("Meal data is null.");
            }
            var existingMeal = _context.Meals.Find(mealId);
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
            _context.SaveChanges();
            return NoContent();
        }
        // Chief delete meal
        [HttpDelete("{chiefId}/meals/{mealId}")]
        public IActionResult DeleteMeal(Guid chiefId, Guid mealId)
        {
            var meal = _context.Meals.Find(mealId);
            if (meal == null)
            {
                return NotFound("Meal not found.");
            }
            _context.Meals.Remove(meal);
            _context.SaveChanges();
            return NoContent();
        }

    }
}