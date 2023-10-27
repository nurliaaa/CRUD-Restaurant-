using CRUD_Restaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodContext _dbContext;

        public FoodController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFood()
        {
            if (_dbContext.Food == null)
            {
                return NotFound();
            }
            return await _dbContext.Food.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            if (_dbContext.Food == null)
            {
                return NotFound();
            }
            var food = await _dbContext.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return food;

        }

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            _dbContext.Food.Add(food);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFood), new { id = food.FoodID }, food);
        }

        [HttpPut]
        public async Task<ActionResult> PutFood(int id, Food food)
        {
            if (id != food.FoodID)
            {
                return BadRequest();
            }
            _dbContext.Entry(food).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool FoodAvailable(int id)
        {
            return (_dbContext.Food?.Any(x => x.FoodID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFood(int id)
        {
            if (_dbContext.Food == null)
            {
                return NotFound();
            }

            var food = await _dbContext.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _dbContext.Food.Remove(food);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
