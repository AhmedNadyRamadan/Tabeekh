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

            IQueryable<Chief> query = _context.Chiefs.Include(c=>c.Meals);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(m => m.Name.Contains(name));
            }

            var totalChiefs = await query.CountAsync();

            var chiefs = await query
                .Select(c=>new{
                    id = c.Id,
                    name = c.Name,
                    totalRate = c.TotalRate,
                    photo = _context.EndUsers.FirstOrDefault(u=>u.Id == c.Id)!.Photo
                })
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
            var chief = await _context.Chiefs.Include(c=>c.Meals)
            .Select(c=>new{
                    id = c.Id,
                    name = c.Name,
                    totalRate = c.TotalRate,
                    photo = _context.EndUsers.FirstOrDefault(u=>u.Id == c.Id)!.Photo,
                    meals = c.Meals.Select(m=>new {
                        id = m.Id,
                        name = m.Name,
                        photo = m.Photo,
                        prepration_Time = m.Prepration_Time,
                        price = m.Price,
                        available = m.Available,
                        ingredients = m.Ingredients,
                        recipe = m.Recipe,
                        measure_unit = m.Measure_unit,
                        totalRate= m.totalRate,
                        day = m.Day,
                        category = _context.Categories.Join(_context.Meals_Categories,c=>c.Id,m=>m.CategoryId,(c,m)=>new {
                            name = c.Name,
                            cid= c.Id,
                            mealId = m.MealId
                    }).FirstOrDefault(c=>c.mealId == m.Id)!.name ?? "",
                    chief_name = m.Chief!.Name
                }),
                    
                }).FirstOrDefaultAsync(c=>c.id == id);
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
                    rate = g.Average(r => r.totalRate),
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

         [HttpGet("{id:guid}/Order")]
        public async Task<ActionResult<IEnumerable<Delivery_Cust_Meal_Order>>> GetOrders(Guid id)
        {
            if (!await _context.Chiefs.AnyAsync(c => c.Id == id))
                return NotFound(new { message = "Chief not found." });

            var orders = await _context.Delivery_Cust_Meal_Orders
                .Include(o=>o.Items)
                .Where(o => o.ChiefId == id)
                .ToListAsync();

            return Ok(orders);
        }

        // Add Meal to Chief
        [HttpPost("Meals/{chiefId}")]
        public async Task<ActionResult<Meal>> AddMealToChief(Guid chiefId, [FromBody] addMealDTO meal)
        {
            Meal mealDB = new Meal();
            Meal_Category meal_Category = new Meal_Category();
            Category Category = new Category();
            var categoryDB = await _context.Categories.FirstOrDefaultAsync(c=>c.Name == meal.Category);

            if (meal == null)
            {
                return BadRequest("Meal data is null.");
            }

            var chief = await _context.Chiefs.FindAsync(chiefId);
            if (chief == null)
            {
                return NotFound("Chief not found.");
            }

            mealDB.Chief_Id = chiefId;

            if(categoryDB != null){
                meal_Category.CategoryId = categoryDB.Id;
            }else{
                Category.Name = meal.Category;
                _context.Categories.Add(Category);
                meal_Category.CategoryId = Category.Id;
            }


            mealDB.Available = meal.Available;
            mealDB.Day = meal.Day;
            mealDB.Ingredients = meal.Ingredients;
            mealDB.Photo = meal.Photo;
            mealDB.Name = meal.Name;
            mealDB.Measure_unit = meal.Measure_unit;
            mealDB.Prepration_Time = meal.Prepration_Time;
            mealDB.Price = meal.Price;
            mealDB.Recipe = meal.Recipe;
            _context.Meals.Add(mealDB);
            await _context.SaveChangesAsync();

            meal_Category.MealId = mealDB.Id;
            _context.Meals_Categories.Add(meal_Category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMealsByChiefId), new { id = chiefId }, mealDB);
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
        [HttpGet("{chiefId}/Reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetChiefReviews(Guid chiefId)
        {
            List<ReviewDTO> reviewsList = new List<ReviewDTO>();
            ReviewDTO review = new ReviewDTO();
            var Reviews = await _context.Cust_Chief_Reviews.Where(r=>r.Chief_Id == chiefId).ToListAsync();
            
            foreach (var rev in Reviews)
            {
                var customer = _context.Customers.FirstOrDefault(c=>c.Id == rev.Customer_Id);
                review.Customer_Name = customer.Name;
                review.Comment = rev.Comment;
                review.totalRate = rev.totalRate;
                reviewsList.Add(review);
                review = new ReviewDTO();
            }
            return Ok(reviewsList);
        }
        [HttpGet("{chiefId}/Rate")]
        public async Task<IActionResult> GetChiefRate(Guid chiefId)
        {
            var Rates = await _context.Cust_Chief_Reviews.Where(r=>r.Chief_Id == chiefId).Select(r=>r.totalRate).ToListAsync();
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