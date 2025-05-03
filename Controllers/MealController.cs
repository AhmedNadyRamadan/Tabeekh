using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;
using Tabeekh.Models;
using DotNetEnv;
using Tabeekh.DTOs;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
         private readonly TabeekhDBContext _context;
    private readonly IConfiguration _config;

    public MealController(TabeekhDBContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
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

            IQueryable<Meal> query = _context.Meals.Include(m=>m.Chief);
            

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
                .Select(m=>new {
                    id = m.Id,
                    name = m.Name,
                    photo = m.Photo,
                    prepration_Time = m.Prepration_Time,
                    price = m.Price,
                    available = m.Available,
                    ingredients = m.Ingredients,
                    recipe = m.Recipe,
                    measure_unit = m.Measure_unit,
                    day = m.Day,
                    totalRate= m.totalRate,
                    chief_name = m.Chief.Name,
                    Category = _context.Meals_Categories.Include(m=>m.Category).FirstOrDefault(u=>u.MealId == m.Id).Category.Name,
                })
                .Skip(limit * (pageNumber - 1))
                .Take(limit)
                .ToListAsync();
            var totalCount = totalMeals;
            return Ok(new{totalCount,items = meals});
        }

        // Get a specific meal by ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Meal>> GetMealById(Guid id)
        {
            var meal = await _context.Meals.Include(m=>m.Chief)
            .Select(m=>new {
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
                        id= c.Id,
                        mealId = m.MealId
                    }).FirstOrDefault(c=>c.mealId == id)!.name ?? "",
                    chief_name = m.Chief!.Name
                }).FirstOrDefaultAsync(m => m.id == id);
            if (meal == null)
            {
                return NotFound(new { message = "Meal not found." });
            }
            return Ok(meal);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Meal>> UpdateMeals([FromBody] UpdateMealDTO updatedMeal,Guid id)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(m=>m.Id == id);
            var category =  await _context.Meals_Categories.FirstOrDefaultAsync(m=>m.MealId == id);
            var categoryId =  _context.Categories.FirstOrDefault(m=>m.Name == updatedMeal.Category)!.Id;
            Meal_Category cat = new Meal_Category();
            if (meal == null)
            {
                return BadRequest("meal not found");
            }
            meal.Name = updatedMeal.Name;
            meal.Available = updatedMeal.Available;
            meal.Day = updatedMeal.Day;
            meal.Price = updatedMeal.Price;

            if (category != null)
            {
                _context.Meals_Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
                cat.CategoryId = categoryId;
                cat.MealId = meal.Id;
                _context.Meals_Categories.Add(cat);
            
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();

            return Ok(meal);
        }

        // Get meals by name
        [HttpGet("{name}")]
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
        [HttpGet("chief/{chiefName}")]
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


        [HttpGet("top10")]
        public async Task<ActionResult<IEnumerable<object>>> GetTop10Meals()
        {
            var topMeals = await _context.Cust_Meal_Reviews
                .GroupBy(r => r.Meal_Id)
                .Select(g => new
                {
                    id = g.Key,
                    name = _context.Meals.FirstOrDefault(c => c.Id == g.Key).Name,
                    rate = g.Average(r => r.totalRate),
                    photo = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Photo,
                    Category = _context.Meals_Categories.Include(m=>m.Category).FirstOrDefault(u=>u.MealId == g.Key).Category.Name,
                    price = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Price,
                    available = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Available,
                    measure_unit = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Measure_unit,
                    chief_name = _context.Meals.Include(m=>m.Chief).FirstOrDefault(u=>u.Id == g.Key).Chief.Name,
                })
                .OrderByDescending(g => g.rate)
                .Take(10)
                .ToListAsync();

         
            return Ok(topMeals);
        }

        [HttpGet("bestOffers")]
        public async Task<ActionResult<IEnumerable<object>>> GetBestOffers()
        {
             Random random = new Random();
            var topMeals = await _context.Cust_Meal_Reviews
                .GroupBy(r => r.Meal_Id)
                .Select(g => new
                {
                    id = g.Key,
                    name = _context.Meals.FirstOrDefault(c => c.Id == g.Key).Name,
                    rate = g.Average(r => r.totalRate),
                    photo = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Photo,
                    Category = _context.Meals_Categories.Include(m=>m.Category).FirstOrDefault(u=>u.MealId == g.Key).Category.Name,
                    oldPrice = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Price,
                    price = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Price * (0.5 + (random.NextDouble() * 0.4)),
                    available = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Available,
                    measure_unit = _context.Meals.FirstOrDefault(u=>u.Id == g.Key).Measure_unit,
                    chief_name = _context.Meals.Include(m=>m.Chief).FirstOrDefault(u=>u.Id == g.Key).Chief.Name,
                })
                .OrderByDescending(g => g.rate)
                .Take(10)
                .ToListAsync();

         
            return Ok(topMeals);
        }


        // Get all reviews for a specific meal
        [HttpGet("Reviews/{mealId:guid}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetMealReviews(Guid mealId)
        {
            var Reviews = await _context.Cust_Meal_Reviews
                .Where(r => r.Meal_Id == mealId)
                .ToListAsync();

             List<ReviewDTO> reviewsList = new List<ReviewDTO>();
            ReviewDTO review = new ReviewDTO();

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

          [HttpGet("Categories/{mealId:guid}")]
        public async Task<ActionResult<IEnumerable<Meal_Category>>> AddCategoryToMeal(Guid mealId)
        {
            var CateDB = await _context.Meals_Categories.Where(c=>c.MealId == mealId).ToListAsync();
            return Ok(CateDB);
        }

        [HttpPost("Categories/{mealId:guid}")]
        public async Task<IActionResult> AddCategoryToMeal(Guid mealId, [FromBody] Category category )
        {
            Meal_Category mealCat = new Meal_Category();
            mealCat.MealId = mealId;

            var CateDB = await _context.Categories.FirstOrDefaultAsync(c=>c.Name == category.Name);
            if(CateDB == null){
                _context.Categories.Add(category);
                mealCat.CategoryId = category.Id;
            }else{
                mealCat.CategoryId = CateDB.Id;
            }
            _context.Meals_Categories.Add(mealCat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddCategoryToMeal),new {id = mealId},CateDB ?? category);
        }
        
        [HttpGet("RecommendFood")]
        public async Task<ActionResult> RecommendFood(string category)
        {
            // var meals = _context.Meals.ToList();
            string prompt = $"Please recommend a meal from the following list based on the category {category} : ";
            // foreach (var meal in meals)
            // {
            //     prompt += $"{meal.Name} , ";
            // }
            prompt += $"please provide an new recipe for me as a chief to make for my customers, make the response in an array like format that has 3 items, each item of that array is an object that has (name, ingredients and recipe) key, also please provide only the array without any extension texts make sure the value is in json format to be able to use it in my front end app don't start with (```json) make sure response in arabic";

            ChatClient client = new(model: "gpt-4o-mini", apiKey: _config.GetValue<string>("Application:OpenAi_Api_key"));
            ChatCompletion completion = client.CompleteChat(prompt);
            return Ok(completion.Content[0].Text);
        }

        [HttpGet("RecommendFoodBasedOnHistory/{customerId:guid}")]
        public async Task<ActionResult> RecommendFoodBasedOnHistory(Guid customerId)
        {
            var orders = await _context.Delivery_Cust_Meal_Orders.Include(o=>o.Items).Where(d=>d.Customer_Id == customerId).ToListAsync();
            string prompt = $"Please recommend a meal from the following list based on the history of orders provided  : ";
            List<string> meals = new List<string>();
            foreach (var order in orders)
            {
                foreach (var mealItem in order.Items){
                var meal = await _context.Meals.FirstOrDefaultAsync(m=> m.Id == mealItem.MealId);
                meals.Add(meal.Name);
                }
            }
            foreach (var item in meals)
            {
                prompt += $"{item}, ";
            }
            
            prompt += $"Please provide the recommended meals in an array format like this: [meal1 details, meal2 details, meal3 details], make the response in an array of objects format and the object contains (name,ingredients,recipe) for each meal";

            ChatClient client = new(model: "gpt-4o-mini", apiKey: _config.GetValue<string>("Application:OpenAi_Api_key"));
            ChatCompletion completion = client.CompleteChat(prompt);
            return Ok(completion.Content[0].Text);
        }
        
        [HttpGet("Rate/{mealId}")]
        public async Task<IActionResult> GetChiefRate(Guid mealId)
        {
            var Rates = await _context.Cust_Meal_Reviews.Where(r=>r.Meal_Id == mealId).Select(r=>r.totalRate).ToListAsync();
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
