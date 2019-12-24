using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelWebApp.Models
{
    public class Room
    {
        public int ID { get; set; }
        //public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public bool IsAvailable { get; set; } = false;
        public DateTime? AvailableDate { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public string ShowRoomNumber 
        { 
            get { return $"{Floor}{RoomNumber}"; } set { RoomNumber = Floor + value; } 
        }

        private string _RoomNumber;

        public string RoomNumber
        {
            get {

                return $"{_RoomNumber}";

            }
            set { _RoomNumber = value; }
        }

    }
}