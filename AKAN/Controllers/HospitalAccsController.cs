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
    public class HospitalAccsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HospitalAccsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HospitalAccs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalAcc>>> GetHospitalAccs()
        {
            return await _context.HospitalAccs.ToListAsync();
        }

        // GET: api/HospitalAccs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalAcc>> GetHospitalAcc(int id)
        {
            var hospitalAcc = await _context.HospitalAccs.FindAsync(id);

            if (hospitalAcc == null)
            {
                return NotFound();
            }

            return hospitalAcc;
        }

        // PUT: api/HospitalAccs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalAcc(int id, HospitalAcc hospitalAcc)
        {
            if (id != hospitalAcc.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalAcc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalAccExists(id))
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

        // POST: api/HospitalAccs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HospitalAcc>> PostHospitalAcc(HospitalAcc hospitalAcc)
        {
            _context.HospitalAccs.Add(hospitalAcc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalAcc", new { id = hospitalAcc.Id }, hospitalAcc);
        }

        // DELETE: api/HospitalAccs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalAcc(int id)
        {
            var hospitalAcc = await _context.HospitalAccs.FindAsync(id);
            if (hospitalAcc == null)
            {
                return NotFound();
            }

            _context.HospitalAccs.Remove(hospitalAcc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HospitalAccExists(int id)
        {
            return _context.HospitalAccs.Any(e => e.Id == id);
        }
    }
}
