namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenderandGenderFKtoUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "GenderId", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "GenderId");
            AddForeignKey("dbo.AspNetUsers", "GenderId", "dbo.Genders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "GenderId", "dbo.Genders");
            DropIndex("dbo.AspNetUsers", new[] { "GenderId" });
            DropColumn("dbo.AspNetUsers", "GenderId");
            DropTable("dbo.Genders");
        }
    }
}
