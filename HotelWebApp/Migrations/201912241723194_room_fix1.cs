namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class room_fix1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rooms", "RoomNumberShow");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "RoomNumberShow", c => c.String());
        }
    }
}
