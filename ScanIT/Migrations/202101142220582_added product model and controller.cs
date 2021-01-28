namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedproductmodelandcontroller : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 400),
                        VAT = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 400),
                        AvailableQuantity = c.Int(nullable: false),
                        Photo = c.Binary(nullable: false),
                        InitialPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BarCode = c.Binary(nullable: false),
                        QRCode = c.Binary(nullable: true),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
