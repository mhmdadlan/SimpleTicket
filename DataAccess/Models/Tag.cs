using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
