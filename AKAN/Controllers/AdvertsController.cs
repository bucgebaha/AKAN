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
using System.Text.Json;
using Newtonsoft.Json;

namespace AKAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdvertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Adverts
        [HttpGet]
        public async Task<ActionResult<Response>> GetAdverts()
        {
            return new Response(true, new { Adverts = await _context.Adverts.ToListAsync() }, null);
        }

        // GET: api/Adverts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetAdvert(int id)
        {
            var advert = await _context.Adverts.FindAsync(id);

            if (advert == null)
            {
                return new Response(false, "", "Verilen Id'ye ait advert bulunamadı");
            }

            var photos = await _context.Photo.Where(x => x.AdvertId == id).ToListAsync();

            var user = await _context.Users.Where(x => x.Id == advert.CreatorID).ToListAsync();

            var hospital = await _context.Hospitals.Where(x => x.Id == advert.HospitalID).ToListAsync();

            var response = new Response(true, new { Advert = advert, AdvertPhotos = photos, AdvertCreator = user, AdvertHospital = hospital }, null);

            return response;
        }

        [HttpGet("CreatedByUser/{id}")]
        public async Task<ActionResult<Response>> GetAdvertByUser(int id)
        {
            var advert = await _context.Adverts.Where(x => x.CreatorID == id).ToListAsync();

            if (!advert.Any())
            {
                return new Response(false, "", "Verilen user'a ait advert bulunamadı");
            }

            var response = new Response(true, new { Adverts = advert }, null);

            return response;
        }

        // PUT: api/Adverts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvert(int id, Advert advert)
        {
            if (id != advert.Id)
            {
                return BadRequest();
            }

            _context.Entry(advert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertExists(id))
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

        // POST: api/Adverts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Advert>> PostAdvert(Advert advert)
        {
            _context.Adverts.Add(advert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvert", new { id = advert.Id }, advert);
        }

        // DELETE: api/Adverts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var advert = await _context.Adverts.FindAsync(id);
            if (advert == null)
            {
                return NotFound();
            }

            _context.Adverts.Remove(advert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdvertExists(int id)
        {
            return _context.Adverts.Any(e => e.Id == id);
        }
    }
}
