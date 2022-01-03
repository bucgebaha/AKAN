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
    public class DistrictsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistrictsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Districts
        [HttpGet]
        public async Task<ActionResult<Response>> GetDistricts()
        {
            return new Response(true, new { Districts = await _context.Districts.ToListAsync() }, null);
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);

            if (district == null)
            {
                return new Response(false, "", "Id'ye ait District bulunamadı.");
            }

            return new Response(true, new { District = district }, null);
        }

        // PUT: api/Districts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(int id, District district)
        {
            if (id != district.Id)
            {
                return BadRequest();
            }

            _context.Entry(district).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
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

        // POST: api/Districts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<District>> PostDistrict(District district)
        {
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrict", new { id = district.Id }, district);
        }

        // DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DistrictExists(int id)
        {
            return _context.Districts.Any(e => e.Id == id);
        }
    }
}
