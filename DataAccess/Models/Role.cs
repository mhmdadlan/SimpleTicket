using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Role : IdentityRole<string, UserRole>
    {
        public Role() : base()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }
}
