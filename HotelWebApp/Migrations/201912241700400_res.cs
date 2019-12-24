namespace HotelWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class res : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "ReservationNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "ReservationNumber", c => c.String());
        }
    }
}
