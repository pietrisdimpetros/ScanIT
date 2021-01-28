namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPriceIncludingVatToOnlyGet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "PriceIncludingVAT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "PriceIncludingVAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
