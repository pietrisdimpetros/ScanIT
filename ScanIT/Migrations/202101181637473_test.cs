namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceIncludingVAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Categories", "VAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Photo", c => c.Binary(nullable: false));
            AlterColumn("dbo.Products", "BarCode", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "BarCode", c => c.Binary());
            AlterColumn("dbo.Products", "Photo", c => c.Binary());
            AlterColumn("dbo.Categories", "VAT", c => c.Single(nullable: false));
            DropColumn("dbo.Products", "PriceIncludingVAT");
        }
    }
}
