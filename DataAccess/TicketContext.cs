using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess
{
    public class TicketContext : IdentityDbContext<ApplicationUser, Role, string, IdentityUserLogin, UserRole, IdentityUserClaim>
    {
        public TicketContext() : base("TicketsDB")
        {
        }
        public static TicketContext Create()
        {
            return new TicketContext();
        }

    }
}
