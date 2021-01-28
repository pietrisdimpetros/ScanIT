namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriceIncludingVatPropertyAndFieldAndChangedVatPropertyInCategoryFromFloatToDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceIncludingVAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Categories", "VAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "VAT", c => c.Single(nullable: false));
            DropColumn("dbo.Products", "PriceIncludingVAT");
        }
    }
}
