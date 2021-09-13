﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonRegistry.Data;
using PersonRegistry.Models;
using PersonRegistry.Interfaces;
using Serilog;


namespace PersonRegistry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            Log.Information("[HttpGet] All");
            return _userRepository.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(string id)
        {
            var user =  _userRepository.Find(id);

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
        public IActionResult PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                Log.Information($"[HttpPut({id})] BadRequest");
                return BadRequest();
            }

            _userRepository.State(user);

            try
            {
                Log.Information($"[HttpPut({id})]");
                _userRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userRepository.UserExists(id))
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
        public ActionResult<User> PostUser(User user)
        {
            _userRepository.Add(user);
            try
            {
                user.Id = _userRepository.IdGenerator();
                user.CreationDate = DateTime.Now;
                Log.Information($"[HttpPost({user.Id})]");
                _userRepository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (_userRepository.UserExists(user.Id))
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
        public IActionResult DeleteUser(string id)
        {
            var user =  _userRepository.Find(id);
            if (user == null)
            {
                Log.Information($"[HttpDelete({id})] NotFound");
                return NotFound();
            }

            Log.Information($"[HttpDelete({id})]");
            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            return NoContent();
        }

    }
}
