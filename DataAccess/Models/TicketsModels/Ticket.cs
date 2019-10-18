using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Assignee> Assignees { get; set; }
        public virtual ICollection<TicketPriority> TicketPriorities { get; set; }
        public virtual ICollection<TicketType> TicketTypes { get; set; }
        public virtual ICollection<TicketStatus> TicketStatuses { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
