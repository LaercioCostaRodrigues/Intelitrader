using PersonRegistry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PersonRegistry.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        //void Put(User user);
        //Task<IActionResult> Put(string id, User user);
        //IActionResult Put(string id, User user);
        //void Put(string id, User user);
        //void PutSaveChanges();
        //Task<IActionResult> PutSaveChanges();

    }
}