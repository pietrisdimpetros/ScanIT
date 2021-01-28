namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changequantitytoselectedquantityinorderdetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "SelectedQuantity", c => c.Single(nullable: true));
            AlterColumn("dbo.OrderDetails", "SelectedQuantity", c => c.Int(nullable: true));

          
            DropColumn("dbo.OrderDetails", "Quantity");
         
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Quantity", c => c.Single(nullable: false));
            DropColumn("dbo.OrderDetails", "SelectedQuantity");
        }
    }
}
