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
    public class DeliveriesController : ControllerBase
    {
        private readonly TabeekhDBContext _context;

        public DeliveriesController(TabeekhDBContext context)
        {
            _context = context;
        }

        // GET: api/Deliveries
        // This endpoint retrieves all deliveries.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            return await _context.Deliveries.ToListAsync();
        }

        // GET: api/Deliveries/ID
        // This endpoint retrieves a specific delivery by ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(Guid id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }

        // PUT: api/Deliveries/ID
        // This endpoint updates a specific delivery by ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(Guid id, Delivery delivery)
        {
            var deliveryDb = await _context.Deliveries.FindAsync(delivery.Id);

            if (deliveryDb == null)
            {
                return BadRequest("Invalid Id");
            }

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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

        // POST: api/Deliveries
        // This endpoint adds a new delivery.
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.Id }, delivery);
        }

        // DELETE: api/Deliveries/ID
        // This endpoint deletes a specific delivery by ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(Guid id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("Orders/{id}")]
        public async Task<ActionResult<IEnumerable<Delivery_Cust_Meal_Order>>> GetDeliveryOrders(Guid id)
        {
            var Orders = await _context.Delivery_Cust_Meal_Orders.Include(o=>o.Items).Where(o=>o.Delivery_Id == id).ToListAsync();
            return Orders;
        }
        // This method checks if a delivery exists by ID.
        private bool DeliveryExists(Guid id)
        {
            return _context.Deliveries.Any(e => e.Id == id);
        }
    }
}
