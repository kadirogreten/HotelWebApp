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
        private string _ReservationNumber;

        public string ShowReservationNumber
        {
            get { return $"{RoomID}-{CustomerID}-{CheckIn.Day}-{CheckOut.Day}"; }
            set { ReservationNumber = value; }
        }

        public string ReservationNumber
        {
            get
            {
                return _ReservationNumber;
            }
            set
            {
                _ReservationNumber = value;
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