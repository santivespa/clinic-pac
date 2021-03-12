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
    public class ConsultationsController : ControllerBase
    {
        private readonly Context _context;

        public ConsultationsController(Context context)
        {
            _context = context;
        }

        // POST: api/Consultations/PostConsultation
        [HttpPost]
        public async Task<Consultation> PostConsultation([FromBody] Consultation consultation)
        {
            if (consultation == null || consultation.User == null)
                return null;
          
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == consultation.User.Email && x.Password == consultation.User.Password && x.Token==consultation.User.Token);

            if (consultation == null || consultation.User == null)
                return null;
            consultation.UserID = user.ID;

            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();
            consultation.User = null;
            return consultation;
        }
        // DELETE: api/Consultations/DeleteConsultation
        [HttpPost]
        public async Task<bool> DeleteConsultation([FromBody] Consultation consultation)
        {
            User user = await _context.Users.Include(x=>x.Consultations).FirstOrDefaultAsync(x => x.Email == consultation.User.Email && x.Password == consultation.User.Password && x.Token == consultation.User.Token);


            var exists = user.Consultations.FirstOrDefault(x=>x.ID==consultation.ID);

            if (exists == null)
            {
                return false;
            }
            _context.Consultations.Remove(exists);
            await _context.SaveChangesAsync();
            return true;
        }

        [HttpPost]
        public async Task<bool> ChangeConsultationState([FromBody] Consultation consultation)
        {
            if (consultation == null || consultation.User == null)
                return false;

            User user = await _context.Users.Include(x => x.Consultations).FirstOrDefaultAsync(x => x.Email == consultation.User.Email && x.Password == consultation.User.Password && x.Token == consultation.User.Token);

            if (consultation == null || consultation.User == null)
                return false;

            var exists = user.Consultations.FirstOrDefault(x => x.ID == consultation.ID);
            if(exists != null)
            {
                exists.State = consultation.State;
                exists.SetCurrentDate();
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
       
          
         
          
        }

    }
}