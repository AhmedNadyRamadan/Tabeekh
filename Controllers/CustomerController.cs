using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly TabeekhDBContext _context;
        public CustomerController(TabeekhDBContext context)
        {
            _context = context;
        }
        // Get all customers
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            // Logic to get all customers
            // For example, you might fetch from a database
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }
        // Get a specific customer by ID
        [HttpGet("{id}")]
        public IActionResult GetCustomer(Guid id)
        {
            // Logic to get a specific customer by ID
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            return Ok(customer);
        }
        // Get all orders for a specific customer
        [HttpGet("GetOrders/{Id:guid}")]
        public IActionResult GetOrders(Guid Id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            var orders = _context.Delivery_Cust_Meal_Orders.Where(o => o.Customer_Id == Id).ToList();
            return Ok(orders);
        }
        // Get all chefs for a specific customer
        [HttpGet("GetChefs/{Id:guid}")]
        public IActionResult GetChefs(Guid Id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            var chefs = _context.Chiefs.ToList();
            return Ok(chefs);
        }
        // Add a new customer
        [HttpPost("Add")]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(new { message = "Customer data is required" });
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }
        // Update an existing customer
        [HttpPut("Update/{id:guid}")]
        public IActionResult UpdateCustomer([FromBody] Customer customer, Guid id)
        {
            var customerDB = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customerDB == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            customerDB.Name = customer.Name ?? customerDB.Name;
            customerDB.Address = customer.Address ?? customerDB.Address;
            customerDB.Email = customer.Email ?? customerDB.Email;
            customerDB.Phone = customer.Phone ?? customerDB.Phone;
            _context.Customers.Update(customerDB);
            _context.SaveChanges();
            return Ok(new { message = "Customer updated successfully", customerDB });
        }
        // Delete a customer
        [HttpDelete("Delete/{id:guid}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var customerDB = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customerDB == null)
            {
                return NotFound(new { message = "Customer not found" });
            }
            _context.Customers.Remove(customerDB);
            _context.SaveChanges();
            return Ok(new { message = "Customer deleted successfully" });
        }
        //get reviews for a specific meal
        [HttpGet("GetMealReviews/{mealId:guid}")]

        public IActionResult GetMealReviews(Guid mealId)
        {
            var meal = _context.Meals.FirstOrDefault(m => m.Id == mealId);
            if (meal == null)
            {
                return NotFound(new { message = "Meal not found" });
            }
            var reviews = _context.Cust_Meal_Reviews.Where(r => r.Meal_Id == mealId).ToList();
            return Ok(reviews);
        }
        // Add Review for a specific meal by id
        [HttpPost("AddMealReview/{mealId:guid}")]
        public IActionResult AddMealReview(Guid mealId, [FromBody] Cust_Meal_Review review)
        {
            var meal = _context.Meals.FirstOrDefault(m => m.Id == mealId);
            if (meal == null)
            {
                return NotFound(new { message = "Meal not found" });
            }
            review.Meal_Id = mealId;
            _context.Cust_Meal_Reviews.Add(review);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMealReviews), new { mealId }, review);
        }
        // Add review for a specific chief by id
        [HttpPost("AddChiefReview/{chiefId:guid}")]
        public IActionResult AddChiefReview(Guid chiefId, [FromBody] Cust_Chief_Review review)
        {
            var chief = _context.Chiefs.FirstOrDefault(c => c.Id == chiefId);
            if (chief == null)
            {
                return NotFound(new { message = "Chief not found" });
            }
            review.Chief_Id = chiefId;
            _context.Cust_Chief_Reviews.Add(review);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetChefs), new { chiefId }, review);
        }
    }
}
