using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabeekh.DTOs;
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
        // This endpoint retrieves all orders.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery_Cust_Meal_Order>>> GetOrders()
        {
            var orders = await _context.Delivery_Cust_Meal_Orders.Include(o=>o.Items).ToListAsync();
          
            return Ok(orders);
        }

        // GET: api/Order/ID
        // This endpoint retrieves a specific order by ID.
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Delivery_Cust_Meal_Order>> GetOrder(Guid id)
        {
            var delivery_Cust_Meal_Order = await _context.Delivery_Cust_Meal_Orders.Include(o=>o.Items).FirstOrDefaultAsync(o=>o.Id == id);

            if (delivery_Cust_Meal_Order == null)
            {
                return NotFound();
            }

            return Ok(delivery_Cust_Meal_Order);
        }

        // PUT: api/Order/ID
        // This endpoint updates a specific order by ID.
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
        // This endpoint adds a new order.
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder(OrderDTO order)
        {
            Delivery_Cust_Meal_Order OrderDB = new Delivery_Cust_Meal_Order();
            var delivery = _context.Deliveries.FirstOrDefault(d => d.Available == true);
            if(delivery == null)
            {
                return BadRequest("there is no free deliveries");
            }

            foreach (var item in order.Items)
            {
                var meal = await _context.Meals.FirstOrDefaultAsync(m=>m.Id == item.MealId);
                if(meal != null){
                    OrderDB.ChiefId = meal.Chief_Id;
                    break;
                }
            }
            OrderDB.Delivery_Id = delivery.Id;
            OrderDB.Address = order.Address;
            OrderDB.Price = order.Price;
            OrderDB.Customer_Id = order.Customer_Id;
            OrderDB.Date = DateTime.Now;
            OrderDB.Address = order.Address;
            OrderDB.Items = order.Items;

            _context.Delivery_Cust_Meal_Orders.Add(OrderDB);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = OrderDB.Id }, OrderDB.Id);
        }

        // DELETE: api/Order/ID
        // This endpoint deletes a specific order by ID.
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
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

        // [HttpGet("GetOrderDetails")]
        // public async Task<ActionResult<IEnumerable<Order_items>>> GetOrderDetails()
        // {
        //     return await _context.Delivery_Cust_Meal_Orders.Select(o=>o.Items).ToListAsync();
        // }

        [HttpGet("GetOrderDetails/{id}")]
        public async Task<ActionResult<IEnumerable<Order_items>>> GetOrderDetails(Guid id)
        {
            var order = await _context.Delivery_Cust_Meal_Orders.Select(o=>new {o.Items,o.Id}).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return Ok();
            }
            return Ok(order);
        }
     

        // This method checks if a specific order exists by ID.
        private bool Delivery_Cust_Meal_OrderExists(Guid id)
        {
            return _context.Delivery_Cust_Meal_Orders.Any(e => e.Id == id);
        }
    }
}
