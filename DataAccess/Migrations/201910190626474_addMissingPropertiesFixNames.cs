namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMissingPropertiesFixNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignees", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Replies", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Assignees", "AssignedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignees", "AssignedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Replies", "CreatedAt");
            DropColumn("dbo.Assignees", "CreatedAt");
        }
    }
}
