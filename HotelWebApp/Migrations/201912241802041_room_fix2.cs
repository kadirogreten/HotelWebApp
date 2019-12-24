namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class room_fix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "ShowRoomNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "ShowRoomNumber");
        }
    }
}
