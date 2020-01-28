using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWebApp.Models
{
    public class Slider
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public byte? Row { get; set; }
        public string Title_1 { get; set; }
        public string Title_2 { get; set; }
    }
}