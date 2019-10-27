using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string hotel { get; set; }
        public int Cost { get; set; }
        public int QuantityNight { get; set; }
        public string TypeOfRooms { get; set; }
        public string Nutrition { get; set; }
        public string Information { get; set; }

        public int ToursId { get; set; }
        public Tour Tours { get; set; }
        public Order Orders { get; set; }
        public TravelBox TravelBoxs { get; set; }

        public ICollection<ImageHotel> ImageHotels { get; set; }
        public Hotel()
        {
            ImageHotels = new List<ImageHotel>();
        }
    }
}
