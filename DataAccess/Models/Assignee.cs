using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Assignee
    {
        public int ID { get; set; }
        public DateTime AssignedAt { get; set; }
        public bool Current { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
