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
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return new Response(false, "", "Id'ye ait User bulunamadı.");
            }

            return new Response(true, new { User = user }, null);
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<Response>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new Response(true, new { CreatedUser = CreatedAtAction("GetUser", new { id = user.Id }, user).Value}, null);
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
