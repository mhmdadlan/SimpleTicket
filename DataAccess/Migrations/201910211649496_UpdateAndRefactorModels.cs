namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAndRefactorModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketPriorities", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketPriorities", "Priority_ID", "dbo.Priorities");
            DropForeignKey("dbo.TicketPriorities", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketStatus", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketStatus", "Status_ID", "dbo.Status");
            DropForeignKey("dbo.TicketStatus", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketTypes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketTypes", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketTypes", "Type_ID", "dbo.Types");
            DropIndex("dbo.TicketPriorities", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketPriorities", new[] { "Priority_ID" });
            DropIndex("dbo.TicketPriorities", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketStatus", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketStatus", new[] { "Status_ID" });
            DropIndex("dbo.TicketStatus", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketTypes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketTypes", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketTypes", new[] { "Type_ID" });
            RenameColumn(table: "dbo.Replies", name: "ApplicationUser_Id", newName: "CreatedByID");
            RenameColumn(table: "dbo.Tickets", name: "ApplicationUser_Id", newName: "CreatedByID");
            RenameColumn(table: "dbo.Assignees", name: "ApplicationUser_Id", newName: "AssignedByID");
            RenameIndex(table: "dbo.Assignees", name: "IX_ApplicationUser_Id", newName: "IX_AssignedByID");
            RenameIndex(table: "dbo.Replies", name: "IX_ApplicationUser_Id", newName: "IX_CreatedByID");
            RenameIndex(table: "dbo.Tickets", name: "IX_ApplicationUser_Id", newName: "IX_CreatedByID");
            CreateTable(
                "dbo.TicketIndicators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedByID = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        Current = c.Boolean(nullable: false),
                        Indicator_ID = c.Int(),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Indicators", t => t.Indicator_ID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .Index(t => t.CreatedByID)
                .Index(t => t.Indicator_ID)
                .Index(t => t.Ticket_ID);
            
            CreateTable(
                "dbo.Indicators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Assignees", "AssignedToID", c => c.String(maxLength: 128));
            AddColumn("dbo.Tickets", "UpdatedAt", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Assignees", "AssignedToID");
            AddForeignKey("dbo.Assignees", "AssignedToID", "dbo.AspNetUsers", "Id");
            DropTable("dbo.TicketPriorities");
            DropTable("dbo.Priorities");
            DropTable("dbo.TicketStatus");
            DropTable("dbo.Status");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.Types");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Assignees", "AssignedToID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketIndicators", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketIndicators", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.TicketIndicators", "Indicator_ID", "dbo.Indicators");
            DropIndex("dbo.TicketIndicators", new[] { "Ticket_ID" });
            DropIndex("dbo.TicketIndicators", new[] { "Indicator_ID" });
            DropIndex("dbo.TicketIndicators", new[] { "CreatedByID" });
            DropIndex("dbo.Assignees", new[] { "AssignedToID" });
            DropColumn("dbo.Tickets", "UpdatedAt");
            DropColumn("dbo.Assignees", "AssignedToID");
            DropTable("dbo.Indicators");
            DropTable("dbo.TicketIndicators");
            RenameIndex(table: "dbo.Tickets", name: "IX_CreatedByID", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Replies", name: "IX_CreatedByID", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Assignees", name: "IX_AssignedByID", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Assignees", name: "AssignedByID", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Tickets", name: "CreatedByID", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Replies", name: "CreatedByID", newName: "ApplicationUser_Id");
            CreateIndex("dbo.TicketTypes", "Type_ID");
            CreateIndex("dbo.TicketTypes", "Ticket_ID");
            CreateIndex("dbo.TicketTypes", "ApplicationUser_Id");
            CreateIndex("dbo.TicketStatus", "Ticket_ID");
            CreateIndex("dbo.TicketStatus", "Status_ID");
            CreateIndex("dbo.TicketStatus", "ApplicationUser_Id");
            CreateIndex("dbo.TicketPriorities", "Ticket_ID");
            CreateIndex("dbo.TicketPriorities", "Priority_ID");
            CreateIndex("dbo.TicketPriorities", "ApplicationUser_Id");
            AddForeignKey("dbo.TicketTypes", "Type_ID", "dbo.Types", "ID");
            AddForeignKey("dbo.TicketTypes", "Ticket_ID", "dbo.Tickets", "ID");
            AddForeignKey("dbo.TicketTypes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TicketStatus", "Ticket_ID", "dbo.Tickets", "ID");
            AddForeignKey("dbo.TicketStatus", "Status_ID", "dbo.Status", "ID");
            AddForeignKey("dbo.TicketStatus", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TicketPriorities", "Ticket_ID", "dbo.Tickets", "ID");
            AddForeignKey("dbo.TicketPriorities", "Priority_ID", "dbo.Priorities", "ID");
            AddForeignKey("dbo.TicketPriorities", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
