namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuantityPropertyAtProductRecipeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductRecipes", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductRecipes", "Quantity");
        }
    }
}
