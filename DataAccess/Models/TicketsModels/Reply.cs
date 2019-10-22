using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Reply
    {
        Reply() { }
        public Reply(string content, Ticket ticket, ApplicationUser createdBy) {
            if (String.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));
            Content = content;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
            CreatedAt = DateTime.Now;
        }
        public int ID { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Ticket Ticket { get; private set; }
        public string CreatedByID { get; private set; }
        [ForeignKey("CreatedByID")]
        public ApplicationUser CreatedBy { get; private set; }
    }
}
