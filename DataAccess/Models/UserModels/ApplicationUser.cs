using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ApplicationUser : IdentityUser<string, IdentityUserLogin, UserRole, IdentityUserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [InverseProperty("CreatedBy")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [InverseProperty("CreatedBy")]
        public virtual ICollection<TicketIndicator> TicketIndicators { get; set; }
        [InverseProperty("AssignedBy")]
        public virtual ICollection<Assignee> TicketsAssignedBy { get; set; }
        [InverseProperty("AssignedTo")]
        public virtual ICollection<Assignee> TicketsAssignedTo { get; set; }
        [InverseProperty("CreatedBy")]
        public virtual ICollection<Reply> Replies { get; set; }

    }
}