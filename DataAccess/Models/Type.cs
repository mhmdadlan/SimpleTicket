using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Type
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TicketType> TicketTypes { get; set; }
    }
}