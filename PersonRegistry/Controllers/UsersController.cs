using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonRegistry.Data;
using PersonRegistry.Models;
using Serilog;

namespace PersonRegistry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PersonRegistryContext _context;

        public UsersController(PersonRegistryContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            Log.Information("[HttpGet] All");
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                Log.Information($"[HttpGet({id})] NotFound");
                return NotFound();
            }

            Log.Information($"[HttpGet({id})]");
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                Log.Information($"[HttpPut({id})] BadRequest");
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                Log.Information($"[HttpPut({id})]");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    Log.Information($"[HttpPut({id})] NotFound");
                    return NotFound();
                }
                else
                {
                    Log.Information($"[HttpPut({id})] throw");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //[Bind("firstName", "surname", "age")]
            _context.User.Add(user);
            try
            {
                user.Id = IdGenerator();
                user.CreationDate = DateTime.Now;
                Log.Information($"[HttpPost({user.Id})]");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    Log.Information($"[HttpPost({user.Id})] Conflict");
                    return Conflict();
                }
                else
                {
                    Log.Information($"[HttpPost({user.Id})] throw");
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                Log.Information($"[HttpDelete({id})] NotFound");
                return NotFound();
            }

            Log.Information($"[HttpDelete({id})]");
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
        
        private string IdGenerator()
        {
            Random random = new Random();
            string id = string.Empty;

            id += (char)random.Next(97, 123);
            id += random.Next(0, 10);
            id += (char)random.Next(97, 123);
            id += random.Next(0, 10);
            id += (char)random.Next(97, 123);

            return id + '-' + id + '-' + id + '-' + id + '-' + id;
        }
    }
}
