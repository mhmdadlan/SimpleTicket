namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRedundantDataInUserRoleModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUserRoles", "ArabicName");
            DropColumn("dbo.AspNetUserRoles", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUserRoles", "Description", c => c.String());
            AddColumn("dbo.AspNetUserRoles", "ArabicName", c => c.String());
        }
    }
}
