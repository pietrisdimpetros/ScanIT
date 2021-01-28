namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductRecipeModelAndDbs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductRecipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeName = c.String(nullable: false),
                        PreparationTime = c.Int(nullable: false),
                        ExecutionTime = c.Int(nullable: false),
                        DifficultyLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductRecipes", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ProductRecipes", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductRecipes", new[] { "RecipeId" });
            DropIndex("dbo.ProductRecipes", new[] { "ProductId" });
            DropTable("dbo.Recipes");
            DropTable("dbo.ProductRecipes");
        }
    }
}
