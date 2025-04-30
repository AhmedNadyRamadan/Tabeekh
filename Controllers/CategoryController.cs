using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabeekh.Models;

namespace Tabeekh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]

    public class CategoryController:ControllerBase
    {
        private readonly TabeekhDBContext _context;
        public CategoryController(TabeekhDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (category == null)
                {
                    return BadRequest("Category data is null.");
                }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpGet]
        public async Task<ActionResult<Category>> GetCategories()
        {
            var category = await _context.Categories.ToListAsync();

            return Ok(category);
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<Category>> GetCategoryById(Guid Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == Id);

            if (category == null)
                {
                    return BadRequest("Category doesn't exist");
                }

            return Ok(category);
        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<Category>> DeleteById(Guid Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == Id);

            if (category == null)
                {
                    return BadRequest("Category doesn't exist");
                }
            string name = category.Name;
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return Ok($"Category {name} deleted successfully");
        }
    }
}