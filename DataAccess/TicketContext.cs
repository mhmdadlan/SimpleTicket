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
        public System.Data.Entity.DbSet<DataAccess.Models.Group> Groups { get; set; }

        public System.Data.Entity.DbSet<DataAccess.Models.Ticket> Tickets { get; set; }
        public System.Data.Entity.DbSet<DataAccess.Models.Assignee> Assignees { get; set; }

        public System.Data.Entity.DbSet<DataAccess.Models.Priority> Priorities { get; set; }
        public System.Data.Entity.DbSet<DataAccess.Models.TicketPriority> TicketPriorities { get; set; }

        public System.Data.Entity.DbSet<DataAccess.Models.Type> Types { get; set; }
        public System.Data.Entity.DbSet<DataAccess.Models.TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<DataAccess.Models.Status> Statuses { get; set; }
        public System.Data.Entity.DbSet<DataAccess.Models.TicketStatus> TicketStatuses { get; set; }

        public System.Data.Entity.DbSet<DataAccess.Models.Tag> Tags { get; set; }

        public System.Data.Entity.DbSet<DataAccess.Models.Reply> Replies { get; set; }

        public static TicketContext Create()
        {
            return new TicketContext();
        }


    }
}
