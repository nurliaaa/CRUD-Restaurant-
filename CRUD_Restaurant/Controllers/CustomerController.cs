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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _dbContext;

        public CustomerController(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            if(_dbContext.Customer == null)
            {
                return NotFound();
            }
            return await _dbContext.Customer.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_dbContext.Customer == null)
            {
                return NotFound();
            }
            var customer = await _dbContext.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;

        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
            {
                _dbContext.Customer.Add(customer);
                await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
            }

        [HttpPut]
        public async Task<ActionResult> PutCustomer(int id, Customer customer)
        {
            if(id != customer.CustomerID)
            {
                return BadRequest();
            }
            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CustomerAvailable(id))
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

        private bool CustomerAvailable(int id)
        {
            return (_dbContext.Customer?.Any(x => x.CustomerID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if(_dbContext.Customer == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.Customer.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            _dbContext.Customer.Remove(customer);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
