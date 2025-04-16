using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly TabeekhDBContext _context;

        public OrderController(TabeekhDBContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery_Cust_Meal_Order>>> GetOrders()
        {
            return await _context.Delivery_Cust_Meal_Orders.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery_Cust_Meal_Order>> GetOrder(int id)
        {
            var delivery_Cust_Meal_Order = await _context.Delivery_Cust_Meal_Orders.FindAsync(id);

            if (delivery_Cust_Meal_Order == null)
            {
                return NotFound();
            }

            return delivery_Cust_Meal_Order;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, Delivery_Cust_Meal_Order delivery_Cust_Meal_Order)
        {
            if (id != delivery_Cust_Meal_Order.Id)
            {
                return BadRequest();
            }

            _context.Entry(delivery_Cust_Meal_Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Delivery_Cust_Meal_OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Delivery_Cust_Meal_Order>> AddOrder(Delivery_Cust_Meal_Order delivery_Cust_Meal_Order)
        {


            var delivery = _context.Deliveries.FirstOrDefault(d => d.Available == true);
            if(delivery == null)
            {
                return BadRequest("there is no free deliveries");
            }
            delivery_Cust_Meal_Order.Delivery_Id = delivery.Id;
            delivery_Cust_Meal_Order.Date = DateTime.Now;
            _context.Delivery_Cust_Meal_Orders.Add(delivery_Cust_Meal_Order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = delivery_Cust_Meal_Order.Id }, delivery_Cust_Meal_Order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var delivery_Cust_Meal_Order = await _context.Delivery_Cust_Meal_Orders.FindAsync(id);
            if (delivery_Cust_Meal_Order == null)
            {
                return NotFound();
            }

            _context.Delivery_Cust_Meal_Orders.Remove(delivery_Cust_Meal_Order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Delivery_Cust_Meal_OrderExists(Guid id)
        {
            return _context.Delivery_Cust_Meal_Orders.Any(e => e.Id == id);
        }
    }
}
