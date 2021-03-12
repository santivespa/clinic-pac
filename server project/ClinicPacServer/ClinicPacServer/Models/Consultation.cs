using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicPacServer.Models
{
    public class Consultation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Surgery { get; set; }
        public DateTime NoTicketDate { get; set; }
        public DateTime TicketDate { get; set; }
        public DateTime PaidDate { get; set; }
        public States State { get; set; }
        public string Prevision { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }

        public string StateSpanish { get; set; }

        public DateTime Date { get; set; }

        public void SetNoTicketDate()
        {
            this.State = States.NoTicket;
            this.NoTicketDate = DateTime.Today;
            this.StateSpanish = "Sin boleta";
            this.Date = NoTicketDate;
        }
        public void SetTicketDate()
        {
            this.State = States.Ticket;
            this.TicketDate = DateTime.Today;
            this.StateSpanish = "Con boleta";
            this.Date = TicketDate;
        }
        public void SetPaidDate()
        {
            this.State = States.Paid;
            this.PaidDate = DateTime.Today;
            this.StateSpanish = "Pagada";
            this.Date = PaidDate;
        }
        public enum States
        {
            NoTicket,
            Ticket,
            Paid
        }
        public Consultation()
        {
            this.NoTicketDate = DateTime.Today;
            this.State = States.NoTicket;
            this.StateSpanish = "Sin boleta";
            this.Date = NoTicketDate;
        }
        public Consultation(string name, string surgery, string doc)
        {
            this.NoTicketDate = DateTime.Today;
            this.State = States.NoTicket;
            this.StateSpanish = "Sin boleta";
            this.Date = NoTicketDate;
            this.Name = name;
            this.Surgery = surgery;
            this.Document = doc;
        }
        public bool NoTicket()
        {
            return this.State == States.NoTicket;
        }
        public bool Ticket()
        {
            return this.State == States.Ticket;
        }
        public bool Paid()
        {
            return this.State == States.Paid;
        }
        public void SetCurrentDate()
        {
            if (this.Ticket())
            {
                this.SetTicketDate();
            }
            else if (this.Paid())
            {
                this.SetPaidDate();
            }
            else if (this.Ticket())
            {
                this.SetNoTicketDate();
            }

        }
    }
}
