using System;

namespace DataAccess.Models
{
    public class TicketPriority
    {
        public int ID { get; set; }
        public Ticket Ticket { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Current { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}