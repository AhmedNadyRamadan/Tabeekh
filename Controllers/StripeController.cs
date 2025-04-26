using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class StripeController : ControllerBase
    {
        private readonly TabeekhDBContext _context;

        public StripeController(TabeekhDBContext context)
        {
            _context = context;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] StripeMealOrderRequest request)
        {
            var domain = "http://localhost:4200";
            var lineItems = new List<SessionLineItemOptions>();
            decimal totalPrice = 0; 

            foreach (var item in request.Items)
            {
                var meal = await _context.Meals.FindAsync(item.MealId);
                if (meal == null || !meal.Available)
                    return BadRequest($"Meal with ID {item.MealId} is not available.");

                var itemTotalPrice = meal.Price * item.Quantity; 
                totalPrice += itemTotalPrice; 

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = meal.Price * 100, // Convert to cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = meal.Name
                        }
                    },
                    Quantity = item.Quantity
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"{domain}/success",
                CancelUrl = $"{domain}/cancel"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            // Return the session ID and the total price in the response
            return Ok(new
            {
                sessionId = session.Id,
                totalPrice = totalPrice, // Return the total price of the order
                currency = "usd" // You can also return the currency for clarity
            });
        }
    }
}
