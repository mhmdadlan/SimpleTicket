using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Tag
    {
        private Tag() { }
        public Tag(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException();
            Title = title;
        }
        public Tag(string title, Ticket ticket) : this(title)
        {
            Tickets = new List<Ticket>() { ticket };
        }
        public int ID { get; private set; }
        public string Title { get; private set; }
        public virtual ICollection<Ticket> Tickets { get; private set; }
    }
}
