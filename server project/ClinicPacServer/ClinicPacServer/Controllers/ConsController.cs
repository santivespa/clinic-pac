using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicPacServer.Models;

namespace ClinicPacServer.Controllers
{
    public class ConsController : Controller
    {
        private readonly Context _context;

        public ConsController(Context context)
        {
            _context = context;
        }

        // GET: Cons
        //public async Task<IActionResult> Index()
        //{
        //    var context = _context.Consultations.Include(c => c.User);
        //    return View(await context.ToListAsync());
        //}

    }
}
