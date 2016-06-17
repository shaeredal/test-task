namespace OnlinerNotifier.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductParameters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OnlinerId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "MaxPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "MinPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "MinPrice");
            DropColumn("dbo.Products", "MaxPrice");
            DropColumn("dbo.Products", "OnlinerId");
        }
    }
}
