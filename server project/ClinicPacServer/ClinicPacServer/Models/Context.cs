using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicPacServer.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Consultation> Consultations { get; set; }


        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}
