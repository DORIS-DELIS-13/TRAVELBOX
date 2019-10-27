using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class ImageHotel
    {
        public int Id { get; set; }
        public string ImageAdress { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotels { get; set; }
    }
}
