namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationNumber = c.String(),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Floor = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        AvailableDate = c.DateTime(),
                        ShowRoomNumber = c.String(),
                        RoomNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Reservations", new[] { "RoomID" });
            DropIndex("dbo.Reservations", new[] { "CustomerID" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
        }
    }
}
