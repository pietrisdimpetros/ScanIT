namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemethodnametopaymentmethodnameinpaymentmethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentMethods", "PaymentMethodName", c => c.String());
            DropColumn("dbo.PaymentMethods", "MethodName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentMethods", "MethodName", c => c.String());
            DropColumn("dbo.PaymentMethods", "PaymentMethodName");
        }
    }
}
