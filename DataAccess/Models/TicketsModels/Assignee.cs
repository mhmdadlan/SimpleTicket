using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Assignee : ICurrent
    {
        private Assignee() { }
        public Assignee(ApplicationUser assignedTo, ApplicationUser assignedBy, Ticket ticket) {
            AssignedTo = assignedTo ?? throw new ArgumentNullException(nameof(assignedTo));
            AssignedBy = assignedBy ?? throw new ArgumentNullException(nameof(assignedBy));
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
            CreatedAt = DateTime.Now;
            Current = true;
        }
        public int ID { get; private set; }
        public string AssignedToID { get; private set; }
        public string AssignedByID { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Current { get; private set; }
        [ForeignKey("AssignedToID")]
        public virtual ApplicationUser AssignedTo { get; private set; }
        [ForeignKey("AssignedByID")]
        public virtual ApplicationUser AssignedBy { get; private set; }
        public virtual Ticket Ticket { get; private set; }

        public void UnCurrent()
        {
            Current = false;
        }
    }
}
