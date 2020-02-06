using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWebApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public string ImagePath { get; set; }
    }
}