using System;

namespace DataAccess.Models
{
    public class TicketType
    {
        public int ID { get; set; }
        public Ticket Ticket { get; set; }
        public Type Type { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Current { get; set; }
    }
}