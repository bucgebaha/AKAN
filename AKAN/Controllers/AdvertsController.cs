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
            var query =
                from advert in _context.Adverts
                join user in _context.Users on advert.CreatorID equals user.Id
                join bloodtype in _context.BloodTypes on user.BloodType equals bloodtype.Id
                select new { 
                    AdvertID = advert.Id,
                    AdvertBloodType = bloodtype.Type,
                    AdvertCreationTime = advert.CreationTime,
                    AdvertDetail = advert.Details,
                    AdvertCreatorId = user.Id,
                    AdvertCreatorName = user.FullName,
                    AdvertCreatorPhoto = user.photoUrl
                };

            return new Response(true, query, null);
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

            var userDetail =
                from user in _context.Users
                join bloodtype in _context.BloodTypes on user.BloodType equals bloodtype.Id
                where user.Id == advert.CreatorID
                select new
                {
                    Id = user.Id,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = user.Password,
                    FullName = user.FullName,
                    BloodType = bloodtype.Type,
                    Location = user.Location,
                    CityId = user.CityId,
                    DistrictId = user.DistrictId,
                    MaxDestination = user.MaxDestination,
                    isAvailable = user.isAvailable,
                    photoUrl = user.photoUrl,
                    CreationTime = user.CreationTime
                };

            var hospital = await _context.Hospitals.Where(x => x.Id == advert.HospitalID).ToListAsync();

            var response = new Response(true, new { Advert = advert, AdvertPhotos = photos, AdvertCreator = userDetail, AdvertHospital = hospital }, null);

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

        [HttpGet("GetAdvertProposals/{id}")]
        public async Task<ActionResult<Response>> GetAdvertProposals(int id)
        {
            var query =
                from advert in _context.Adverts
                join proposal in _context.Proposals on advert.Id equals proposal.AdvertId
                join user in _context.Users on proposal.TransmitterId equals user.Id
                where advert.Id == id
                select new
                {
                    AdvertId = advert.Id,
                    ProposalId = proposal.Id,
                    TransmitterName = user.FullName,
                    TransmitterPhoto = user.photoUrl,
                    TransmitterId = user.Id,
                    IsProposalAccepted = proposal.isAccepted,
                    ChatId = proposal.ChatId,
                    ProposalCreationTime = proposal.CreationTime
                };

            if (query.Any())
            {
                return new Response(true, query, null);
            }
            else return new Response(false, "", "İlana ait başvuru bulunamadı!");
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
        public async Task<ActionResult<Response>> PostAdvert(Advert advert)
        {
            _context.Adverts.Add(advert);
            await _context.SaveChangesAsync();

            return new Response(true, new { Advert = CreatedAtAction("GetAdvert", new { id = advert.Id }, advert).Value }, "");
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
