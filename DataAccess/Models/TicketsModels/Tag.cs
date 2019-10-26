using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        public int ID { get; private set; }
        [StringLength(450)]
        public string Title { get; private set; }
        public virtual ICollection<Ticket> Tickets { get; private set; } = new List<Ticket>();
    }
}
