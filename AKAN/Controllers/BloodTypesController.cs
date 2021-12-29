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
        public async Task<ActionResult<IEnumerable<BloodType>>> GetBloodTypes()
        {
            return await _context.BloodTypes.ToListAsync();
        }

        // GET: api/BloodTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodType>> GetBloodType(int id)
        {
            var bloodType = await _context.BloodTypes.FindAsync(id);

            if (bloodType == null)
            {
                return NotFound();
            }

            return bloodType;
        }

        private bool BloodTypeExists(int id)
        {
            return _context.BloodTypes.Any(e => e.Id == id);
        }
    }
}
