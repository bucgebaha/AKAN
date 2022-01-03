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
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatRooms
        [HttpGet]
        public async Task<ActionResult<Response>> GetChatRooms()
        {
            return new Response(true, new { ChatRooms = await _context.ChatRooms.ToListAsync() }, null);
        }

        // GET: api/ChatRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetChatRoom(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);

            if (chatRoom == null)
            {
                return new Response(false, "", "Id'ye ait ChatRoom bulunamadı.");
            }

            return new Response(true, new { ChatRoom = chatRoom }, null); ;
        }

        [HttpGet("GetChatRoomWithUser/{myId}/{targetUserId}")]
        public async Task<ActionResult<Response>> GetChatRoomWithUser(int myId, int targetUserId)
        {
            var chatRoom = await _context.ChatRooms.Where(x => (x.TransmitterId == targetUserId || x.TransmitterId == myId) 
                                                            && (x.ReceiverId == myId || x.ReceiverId == targetUserId)).ToListAsync();

            if (!chatRoom.Any())
            {
                return new Response(false, "", "Bu iki kullanıcıya ait ChatRoom bulunamadı.");
            }

            return new Response(true, new { ChatRoom = chatRoom }, null); ;
        }

        // PUT: api/ChatRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatRoom(int id, ChatRoom chatRoom)
        {
            if (id != chatRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatRoomExists(id))
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

        [HttpPut("ResetUnreadMessages/{id}")]
        public async Task<ActionResult<Response>> PutChatRoomMessageCount(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);

            if (chatRoom == null)
            {
                return new Response(false, "", "Id'ye ait ChatRoom bulunamadı.");
            }

            chatRoom.UnreadMessageCount = 0;
            _context.Entry(chatRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatRoomExists(id))
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

        [HttpPut("NewMessage/{id}")]
        public async Task<ActionResult<Response>> PutChatRoomNewMessage(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);

            if (chatRoom == null)
            {
                return new Response(false, "", "Id'ye ait ChatRoom bulunamadı.");
            }

            chatRoom.UnreadMessageCount += 1;
            _context.Entry(chatRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatRoomExists(id))
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

        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

        // DELETE: api/ChatRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatRoom(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            _context.ChatRooms.Remove(chatRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatRoomExists(int id)
        {
            return _context.ChatRooms.Any(e => e.Id == id);
        }
    }
}
