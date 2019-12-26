namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "ShowReservationNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "ShowReservationNumber");
        }
    }
}
