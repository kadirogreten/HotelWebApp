namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class room_fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomNumberShow", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "RoomNumberShow");
        }
    }
}
