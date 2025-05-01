using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // GET: api/Customers
        // This endpoint retrieves all customers.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET: api/Customers/{id}
        // This endpoint retrieves a specific customer by ID.
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            return Ok(customer);
        }

        // GET: api/Customers/{id}/orders
        // This endpoint retrieves all orders for a specific customer.
        [HttpGet("{id:guid}/orders")]
        public async Task<ActionResult<IEnumerable<Delivery_Cust_Meal_Order>>> GetOrders(Guid id)
        {
            if (!await _context.Customers.AnyAsync(c => c.Id == id))
                return NotFound(new { message = "Customer not found." });

            var orders = await _context.Delivery_Cust_Meal_Orders
                .Include(o=>o.Order_items)
                .Where(o => o.Customer_Id == id)
                .ToListAsync();

            return Ok(orders);
        }
        // // !deleted
        // // GET: api/Customers/{id}/chefs
        // // This endpoint retrieves all chefs for a specific customer.
        // [HttpGet("{id:guid}/chiefs")]
        // public async Task<ActionResult<IEnumerable<Chief>>> GetChefs(Guid id)
        // {
        //     if (!await _context.Customers.AnyAsync(c => c.Id == id))
        //         return NotFound(new { message = "Customer not found." });

        //     var chiefs = await _context.Chiefs.ToListAsync();
        //     return Ok(chiefs);
        // }

        // POST: api/Customers
        // This endpoint adds a new customer.
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest(new { message = "Customer data is required." });

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/Customers/{id}
        // This endpoint updates an existing customer.
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            var customerDB = await _context.Customers.FindAsync(id);
            if (customerDB == null)
                return NotFound(new { message = "Customer not found." });

            customerDB.Name = customer.Name ?? customerDB.Name;
            customerDB.Address = customer.Address ?? customerDB.Address;
            customerDB.Email = customer.Email ?? customerDB.Email;
            customerDB.Phone = customer.Phone ?? customerDB.Phone;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer updated successfully.", customer = customerDB });
        }

        // DELETE: api/Customers/{id}
        // This endpoint deletes a specific customer by ID.
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer deleted successfully." });
        }

        // POST: api/Customers/meals/{mealId}/reviews
        // This endpoint adds a review for a specific meal.
        [HttpPost("meals/{mealId:guid}/reviews")]
        public async Task<IActionResult> AddMealReview(Guid mealId, [FromBody] Cust_Meal_Review review)
        {
            var mealExists = await _context.Meals.AnyAsync(m => m.Id == mealId);
            if (!mealExists)
                return NotFound(new { message = "Meal not found." });

            review.Meal_Id = mealId;
            _context.Cust_Meal_Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        // POST: api/Customers/chiefs/{chiefId}/reviews
        // This endpoint adds a review for a specific chief.
        [HttpPost("chiefs/{chiefId:guid}/reviews")]
        public async Task<IActionResult> AddChiefReview(Guid chiefId, [FromBody] Cust_Chief_Review review)
        {
            var chiefExists = await _context.Chiefs.AnyAsync(c => c.Id == chiefId);
            if (!chiefExists)
                return NotFound(new { message = "Chief not found." });

            review.Chief_Id = chiefId;
            _context.Cust_Chief_Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

    }
}
