using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelWebApp.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public string ReservationNumber
        {
            get
            {
                return $"{RoomID}-{CustomerID}-{CheckIn.Day}-{CheckOut.Day}";
            }
            set
            {
                ReservationNumber = $"{RoomID}-{CustomerID}-{CheckIn.Day}-{CheckOut.Day}";
            }
        }



        [DataType(DataType.DateTime)]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CheckOut { get; set; }
        public bool IsComplete { get; set; } = false;

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int RoomID { get; set; }
        public Room Room { get; set; }
    }
}