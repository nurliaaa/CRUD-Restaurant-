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
    public class TransaksiController : ControllerBase
    {
        private readonly TransaksiContext _dbContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaksi>>> GetTransaksi()
        {
            var transaksiList = await _dbContext.Transaksi
                .Include(t => t.Food)
                .Include(t => t.Customer)
                .ToListAsync();

            if (transaksiList == null)
            {
                return NotFound();
            }

            if (transaksiList.Count == 0)
            {
                return NotFound("No transactions found."); 
            }

            return transaksiList;
        }


        public TransaksiController(TransaksiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaksi>> GetTransaksi(int id)
        {
            if (_dbContext.Transaksi == null)
            {
                return NotFound();
            }
            var transaksi = await _dbContext.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound();
            }
            return transaksi;

        }

        [HttpPost]
        public async Task<ActionResult<Transaksi>> PostTransaksi(Transaksi transaksi)
        {
            _dbContext.Transaksi.Add(transaksi);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaksi), new { id = transaksi.TransaksiID }, transaksi);
        }

        [HttpPut]
        public async Task<ActionResult> PutTransaksi(int id, Transaksi transaksi)
        {
            if (id != transaksi.TransaksiID)
            {
                return BadRequest();
            }
            _dbContext.Entry(transaksi).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaksiAvailable(id))
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
        private bool TransaksiAvailable(int id)
        {
            return (_dbContext.Transaksi?.Any(x => x.TransaksiID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaksi(int id)
        {
            if (_dbContext.Transaksi == null)
            {
                return NotFound();
            }

            var transaksi = await _dbContext.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound();
            }

            _dbContext.Transaksi.Remove(transaksi);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
