namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeinpaymentstatusnameandorderstatusname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderStatus", "OrderStatusName", c => c.String());
            AddColumn("dbo.PaymentStatus", "PaymentStatusName", c => c.String());
            DropColumn("dbo.OrderStatus", "StatusName");
            DropColumn("dbo.PaymentStatus", "StatusName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentStatus", "StatusName", c => c.String());
            AddColumn("dbo.OrderStatus", "StatusName", c => c.String());
            DropColumn("dbo.PaymentStatus", "PaymentStatusName");
            DropColumn("dbo.OrderStatus", "OrderStatusName");
        }
    }
}
