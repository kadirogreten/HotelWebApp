using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWebApp.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}