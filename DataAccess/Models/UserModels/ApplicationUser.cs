using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ApplicationUser : IdentityUser<string, IdentityUserLogin, UserRole, IdentityUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TicketStatus> TicketStatuses { get; set; }
        public ICollection<TicketType> TicketTypes { get; set; }
        public ICollection<TicketPriority> TicketPriorities { get; set; }
        public ICollection<Reply> Replies { get; set; }

    }
}