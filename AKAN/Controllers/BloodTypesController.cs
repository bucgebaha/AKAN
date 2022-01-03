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
    public class BloodTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloodTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BloodTypes
        [HttpGet]
        public async Task<ActionResult<Response>> GetBloodTypes()
        {
            return new Response(true, new { BloodTypes = await _context.BloodTypes.ToListAsync() }, null);
        }

        // GET: api/BloodTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetBloodType(int id)
        {
            var bloodType = await _context.BloodTypes.FindAsync(id);

            if (bloodType == null)
            {
                return new Response(false, "", "Id'ye ait BloodType bulunamadı.");
            }

            return new Response(true, new { BloodType = bloodType }, null);
        }

        [HttpPost]
        public async Task<ActionResult<BloodType>> PostBloodType(BloodType bloodType)
        {
            _context.BloodTypes.Add(bloodType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodType", new { id = bloodType.Id }, bloodType);
        }

        private bool BloodTypeExists(int id)
        {
            return _context.BloodTypes.Any(e => e.Id == id);
        }
    }
}
