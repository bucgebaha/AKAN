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

namespace AKAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<Response>> GetUsers()
        {
            return new Response(true, new { Users = await _context.Users.ToListAsync() }, null);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetUser(int id)
        {
            var query =
                from user in _context.Users
                join bloodtype in _context.BloodTypes on user.BloodType equals bloodtype.Id
                where user.Id == id
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

            if (query.Any())
            {
                return new Response(true, query, null);
            }
            else
            {
                return new Response(false, null, "Kullanıcı bulunamadı!");
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        public class UserUpdate
        {
            public int UserId { get; set; }
            public string? newDataString { get; set; }
            public int? newDataInt { get; set; }
        }
        [HttpPut("UpdatePassword")]
        public async Task<ActionResult<Response>> PutUserPassword([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.Password = userUpdate.newDataString;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "Şifre güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("UpdateEmail")]
        public async Task<ActionResult<Response>> PutUserEmail([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.Email = userUpdate.newDataString;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "E-Mail güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("UpdateFullName")]
        public async Task<ActionResult<Response>> PutUserFullName([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.FullName = userUpdate.newDataString;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "İsim güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("UpdatePhone")]
        public async Task<ActionResult<Response>> PutUserPhone([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.Phone = userUpdate.newDataString;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "Telefon numarası güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("UpdateCity")]
        public async Task<ActionResult<Response>> PutUserCity([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.CityId = userUpdate.newDataInt;
            user.DistrictId = null;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "Telefon numarası güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("UpdateDistrict")]
        public async Task<ActionResult<Response>> PutUserDistrict([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.DistrictId = userUpdate.newDataInt;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "Telefon numarası güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("ChangeProfilePhoto")]
        public async Task<ActionResult<Response>> ChangeProfilePhoto([FromBody] UserUpdate userUpdate)
        {
            var user = await _context.Users.FindAsync(userUpdate.UserId);
            user.photoUrl = userUpdate.newDataString;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "Profil fotoğrafı güncellendi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userUpdate.UserId))
                {
                    return new Response(false, "", "User doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<Response>> PostUser(User user)
        {
            var user2 = await _context.Users.Where(x => x.Email == user.Email).ToListAsync();

            if (user2.Any())
            {
                return new Response(false, "", "Email zaten kayıtlı!");
            }
            else
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new Response(true, new { CreatedUser = CreatedAtAction("GetUser", new { id = user.Id }, user).Value }, null);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<Response>> LoginUser([FromBody] UserLogin userLogin)
        {
            var user = await _context.Users.Where(x => x.Email == userLogin.Email).ToListAsync();

            if (!user.Any())
            {
                return new Response(false, "", "Email kayıtlı değil.");
            }

            if (user[0].Password == userLogin.Password) return new Response(true, new { User = user }, "");
            else return new Response(false, "", "Yanlış Şifre!");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
