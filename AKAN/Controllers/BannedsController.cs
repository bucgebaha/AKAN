#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AKAN.Data;
using AKAN.Models;

namespace AKAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BannedsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BannedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Banneds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banned>>> GetBanned()
        {
            return await _context.Banned.ToListAsync();
        }

        // GET: api/Banneds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Banned>> GetBanned(int id)
        {
            var banned = await _context.Banned.FindAsync(id);

            if (banned == null)
            {
                return NotFound();
            }

            return banned;
        }

        // PUT: api/Banneds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBanned(int id, Banned banned)
        {
            if (id != banned.Id)
            {
                return BadRequest();
            }

            _context.Entry(banned).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannedExists(id))
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

        // POST: api/Banneds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Banned>> PostBanned(Banned banned)
        {
            _context.Banned.Add(banned);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBanned", new { id = banned.Id }, banned);
        }

        // DELETE: api/Banneds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanned(int id)
        {
            var banned = await _context.Banned.FindAsync(id);
            if (banned == null)
            {
                return NotFound();
            }

            _context.Banned.Remove(banned);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BannedExists(int id)
        {
            return _context.Banned.Any(e => e.Id == id);
        }
    }
}
