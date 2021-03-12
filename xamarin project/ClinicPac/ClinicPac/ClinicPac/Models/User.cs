using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPac.Models
{
    public class User
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
        public string Token { get; set; }
        public bool Active { get; set; }
        public string ActiveCode { get; set; }
        [Ignore]
        public List<Consultation> Consultations { get; set; } = new List<Consultation>();
        [Ignore]
        public string NewPass { get; set; }
        [Ignore]
        public string NewEmail { get; set; }
        public Roles Rol { get; set; }
       
        public enum Roles
        {
            Client,
            Admin
        }

        public User()
        {
            this.Rol = Roles.Client;
            this.Active = false;
        }
        public bool ActiveUser(string code)
        {
            if(code == this.ActiveCode)
            {
                this.Active = true;
                return true;
            }
            return false;
        }
        public static string CreateCode(int large)
        {
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < large--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

    }
}
