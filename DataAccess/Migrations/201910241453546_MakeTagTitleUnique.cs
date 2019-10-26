namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTagTitleUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Title", c => c.String(maxLength: 450));
            CreateIndex("dbo.Tags", "Title", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "Title" });
            AlterColumn("dbo.Tags", "Title", c => c.String());
        }
    }
}
