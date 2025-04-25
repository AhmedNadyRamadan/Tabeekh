using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly TabeekhDBContext _context;
        public MealController(TabeekhDBContext context)
        {
            _context = context;
        }

        // Get all meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetAllMeals(
            [FromQuery] int pageNumber,
            [FromQuery] int limit,
            [FromQuery] string name = "",
            [FromQuery] string category = ""
            
            )
        {
            if (pageNumber <= 0 || limit <= 0)
            {
                return BadRequest(new { message = "Page number and limit must be greater than zero." });
            }

            IQueryable<Meal> query = _context.Meals;
            

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(m => m.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                var cat = await _context.Categories.FirstOrDefaultAsync(c=>c.Name.Contains(category));
                if(cat == null){
                    return BadRequest("wrong category");
                }
                var mealsWithCat = await _context.Meals_Categories.Where(m=>m.CategoryId == cat.Id).Select(m=>m.MealId).ToListAsync();
                query = query.Where(m=>mealsWithCat.Contains(m.Id));
            }

            var totalMeals = await query.CountAsync();

            var meals = await query
                .Skip(limit * (pageNumber - 1))
                .Take(limit)
                .ToListAsync();

            var paginationMetadata = new
            {
                totalCount = totalMeals,
                pageNumber,
                pageSize = limit,
                totalPages = (int)Math.Ceiling(totalMeals / (double)limit)
            };

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(meals);
        }

        // Get a specific meal by ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Meal>> GetMealById(Guid id)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound(new { message = "Meal not found." });
            }
            return Ok(meal);
        }

        // Get meals by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByName(string name)
        {
            var meals = await _context.Meals
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            //if (meals == null || !meals.Any())
            //{
            //    return NotFound(new { message = "No meals found with the given name." });
            //}
            return Ok(meals);
        }

        // Get meals by chief ID
        [HttpGet("chief/{chiefId:guid}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByChiefId(Guid chiefId)
        {
            var meals = await _context.Meals
                .Where(m => m.Chief_Id == chiefId)
                .ToListAsync();

            if (meals == null || !meals.Any())
            {
                return NotFound(new { message = "No meals found for the given chief ID." });
            }
            return Ok(meals);
        }

        // Get meals by chief name
        [HttpGet("chief/name/{chiefName}")]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMealsByChiefName(string chiefName)
        {
            var meals = await _context.Meals
                .Where(m => m.Chief.Name.ToLower().Contains(chiefName.ToLower()))
                .ToListAsync();
            //if (meals == null || !meals.Any())
            //{
            //    return NotFound(new { message = "No meals found for the given chief name." });
            //}
            return Ok(meals);
        }

        // Get all reviews for a specific meal
        [HttpGet("GetMealReviews/{mealId:guid}")]
        public async Task<ActionResult<IEnumerable<Cust_Meal_Review>>> GetMealReviews(Guid mealId)
        {
            var mealExists = await _context.Meals.AnyAsync(m => m.Id == mealId);
            if (!mealExists)
            {
                return NotFound(new { message = "Meal not found." });
            }

            var reviews = await _context.Cust_Meal_Reviews
                .Where(r => r.Meal_Id == mealId)
                .ToListAsync();

            return Ok(reviews);
        }
        
    }
}
