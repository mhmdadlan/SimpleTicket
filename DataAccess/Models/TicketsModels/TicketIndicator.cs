using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class TicketIndicator : ICurrent
    {
        private TicketIndicator()
        {

        }
        public TicketIndicator(Ticket ticket, ApplicationUser user, Indicator indicator)
        {
            Ticket = ticket;
            CreatedBy = user;
            Indicator = indicator;
            CreatedAt = DateTime.Now;
            Current = true;
        }
        public int ID { get; private set; }
        public Ticket Ticket { get; private set; }
        public string CreatedByID { get; private set; }
        [ForeignKey("CreatedByID")]
        public ApplicationUser CreatedBy { get; private set; }
        public Indicator Indicator { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Current { get; private set; }

        public void UnCurrent()
        {
            this.Current = false;
        }
    }
}