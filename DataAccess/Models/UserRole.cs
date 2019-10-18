using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public UserRole() : base() { }

        public string ArabicName { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Role Role { get; set; }

    }
}
