namespace DataAccess.Migrations
{
    using DataAccess.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.TicketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DataAccess.TicketContext";
        }

        protected override void Seed(DataAccess.TicketContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Indicators.AddOrUpdate(i => i.Title,
                new Indicator("Open", "The ticket is under proccessing", IndicatorName.Status),
                new Indicator("Pending", "The ticket is pending", IndicatorName.Status),
                new Indicator("Solved", "The ticket has been solved", IndicatorName.Status));
            context.Indicators.AddOrUpdate(i => i.Title,
                new Indicator("Low", "Low ticket priority", IndicatorName.Priority),
                new Indicator("Normal", "Normal ticket priority", IndicatorName.Priority),
                new Indicator("High", "High ticket priority", IndicatorName.Priority),
                new Indicator("Urgent", "Urgent ticket priority", IndicatorName.Priority));
            context.Indicators.AddOrUpdate(i => i.Title,
                new Indicator("Question", "Ask a question", IndicatorName.Type),
                new Indicator("Incident", "Report an incident", IndicatorName.Type),
                new Indicator("Problem", "Report a problem", IndicatorName.Type));
            context.Roles.AddOrUpdate(p => p.Name, new Role { Name = "Admin", Description = "Administrator Role" },
                new Role { Name = "Assignor", Description = "Ticket Assignor role" },
                new Role { Name = "Assignee", Description = "Ticket Proccessor Role" },
                new Role { Name = "Issuer", Description = "Ticket Issuer Role" });
        }
    }
}
