using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TicketStatus
    {
        public int ID { get; set; }
        public Ticket Ticket { get; set; }
        public Status Status { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Current { get; set; }
    }
}
