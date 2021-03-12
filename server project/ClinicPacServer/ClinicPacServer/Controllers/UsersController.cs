using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicPacServer.Models;

namespace ClinicPacServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<object> Login(string Email, string Pass)
        {

            User user = _context.Users.Include(x => x.Consultations).FirstOrDefault(u => u.Email == Email && u.Password == Pass);


            if (user != null)
            {
                string token = Models.User.CreateCode(20);
                user.Token = token;
                await _context.SaveChangesAsync();
                user.Consultations.ForEach(x => { x.User = null; });
                return user;
            }





            return null;
        }
        [HttpPost]
        public async Task<User> Edit([FromBody] User user)
        {
            if (user == null)
                return null;

            User exists = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password && x.Token == user.Token);

            if (exists == null)
                return null;


            if (!String.IsNullOrEmpty(user.NewPass))
            {
                exists.Password = user.NewPass;
            }
            if (!String.IsNullOrEmpty(user.NewEmail))
            {
                exists.Email = user.NewEmail;
            }
          
            exists.Alias = user.Alias;
            exists.Name = user.Name;

            await _context.SaveChangesAsync();
            return exists;

        }
    }
}