namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUpdateDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
