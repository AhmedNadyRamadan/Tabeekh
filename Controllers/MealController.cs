using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllMeals()
        {
            var meals = _context.Meals.ToList();
            if (meals == null || !meals.Any())
            {
                return NotFound("No meals found.");
            }
            return Ok(meals);
        }
        // Get meal by ID
        [HttpGet("{id}")]
        public IActionResult GetMealById(Guid id)
        {
            var meal = _context.Meals.FirstOrDefault(m => m.Id == id);
            if (meal == null)
            {
                return NotFound("Meal not found.");
            }
            return Ok(meal);
        }
        // Get meals by name
        [HttpGet("name/{name}")]
        public IActionResult GetMealsByName(string name)
        {
            var meals = _context.Meals.Where(m => m.Name.ToLower().Contains(name.ToLower())).ToList();
            if (meals == null || !meals.Any())
            {
                return NotFound("No meals found with the given name.");
            }
            return Ok(meals);
        }
        // Get meals by chief ID
        [HttpGet("chief/{chiefId}")]
        public IActionResult GetMealsByChiefId(Guid chiefId)
        {
            var meals = _context.Meals.Where(m => m.Chief_Id == chiefId).ToList();
            if (meals == null || !meals.Any())
            {
                return NotFound("No meals found for the given chief ID.");
            }
            return Ok(meals);
        }

    }
}
