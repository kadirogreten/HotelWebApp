using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelWebApp.Models
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext() : base("name=HotelDb") { }


        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Room> Room { get; set; }
    }
}