namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsInactivePropertyToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsInactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsInactive");
        }
    }
}
