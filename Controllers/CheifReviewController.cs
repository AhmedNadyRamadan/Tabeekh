using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheifReviewController : ControllerBase
    {
        private readonly TabeekhDBContext _context;
        public CheifReviewController(TabeekhDBContext context)
        {
            _context = context;
        }
        // Get all reviews for a specific chief
        [HttpGet("{chiefId}")]
        public IActionResult GetReviewsByChiefId(Guid chiefId)
        {
            var reviews = _context.Cust_Chief_Reviews.Where(r => r.Chief_Id == chiefId).ToList();
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for this chief.");
            }
            return Ok(reviews);
        }

    }
}
