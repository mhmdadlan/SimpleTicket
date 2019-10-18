namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureTicketsModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssignedAt = c.DateTime(nullable: false),
                        Current = c.Boolean(nullable: false),
                        Ticket_ID = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Ticket_ID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Ticket_ID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Details = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TicketPriorities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Current = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Priority_ID = c.Int(),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Priorities", t => t.Priority_ID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Priority_ID)
                .Index(t => t.Ticket_ID);
            
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TicketStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Current = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Status_ID = c.Int(),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Status", t => t.Status_ID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Status_ID)
                .Index(t => t.Ticket_ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Current = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Ticket_ID = c.Int(),
                        Type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .ForeignKey("dbo.Types", t => t.Type_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Ticket_ID)
                .Index(t => t.Type_ID);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagTickets",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Ticket_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Ticket_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Ticket_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketTypes", "Type_ID", "dbo.Types");
            DropForeignKey("dbo.TicketTypes", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketTypes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketStatus", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketStatus", "Status_ID", "dbo.Status");
            DropForeignKey("dbo.TicketStatus", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketPriorities", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketPriorities", "Priority_ID", "dbo.Priorities");
            DropForeignKey("dbo.TicketPriorities", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagTickets", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TagTickets", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.Replies", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.Assignees", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TagTickets", new[] { "Ticket_ID" });
            DropIndex("dbo.TagTickets", new[] { "Tag_ID" });
            DropIndex("dbo.TicketTypes", new[] { "Type_ID" });
            DropIndex("dbo.TicketTypes", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketTypes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketStatus", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketStatus", new[] { "Status_ID" });
            DropIndex("dbo.TicketStatus", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketPriorities", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketPriorities", new[] { "Priority_ID" });
            DropIndex("dbo.TicketPriorities", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Tickets", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Replies", new[] { "Ticket_ID" });
            DropIndex("dbo.Replies", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Assignees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Assignees", new[] { "Ticket_ID" });
            DropTable("dbo.TagTickets");
            DropTable("dbo.Types");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.Status");
            DropTable("dbo.TicketStatus");
            DropTable("dbo.Priorities");
            DropTable("dbo.TicketPriorities");
            DropTable("dbo.Tags");
            DropTable("dbo.Tickets");
            DropTable("dbo.Replies");
            DropTable("dbo.Assignees");
        }
    }
}
