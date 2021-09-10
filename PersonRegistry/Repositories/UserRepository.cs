using PersonRegistry.Data;
using PersonRegistry.Models;
using PersonRegistry.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Serilog;

using Microsoft.AspNetCore.Http;


using System;
using System.Linq;

namespace PersonRegistry.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PersonRegistryContext _context;

        public UserRepository(PersonRegistryContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.User.FindAsync(id);
        }
        

/*


        public IActionResult Put(string id, User user)
        //public void Put(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                Log.Information($"[HttpPut({id})]");
                _context.SaveChangesAsync();
                //_userRepository.SaveChangesAsync();
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

*/

/*
       //public async Task<IActionResult> Put(string id, User user)
        public void Put(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<IActionResult> PutSaveChanges()
        //public void PutSaveChanges()
        {
           _context.SaveChangesAsync();
        }
*/



        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}