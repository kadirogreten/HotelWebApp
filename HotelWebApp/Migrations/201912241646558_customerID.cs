namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.Reservations", new[] { "Customer_ID" });
            RenameColumn(table: "dbo.Reservations", name: "Customer_ID", newName: "CustomerID");
            AlterColumn("dbo.Reservations", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "CustomerID");
            AddForeignKey("dbo.Reservations", "CustomerID", "dbo.Customers", "ID", cascadeDelete: true);
            DropColumn("dbo.Reservations", "CustmerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "CustmerID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reservations", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Reservations", new[] { "CustomerID" });
            AlterColumn("dbo.Reservations", "CustomerID", c => c.Int());
            RenameColumn(table: "dbo.Reservations", name: "CustomerID", newName: "Customer_ID");
            CreateIndex("dbo.Reservations", "Customer_ID");
            AddForeignKey("dbo.Reservations", "Customer_ID", "dbo.Customers", "ID");
        }
    }
}
