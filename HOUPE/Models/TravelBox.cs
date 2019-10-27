using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class TravelBox
    {
        public int Id { get; set; }
        public int Nutrition { get; set; }
        public int Room { get; set; }
        public int Enterteiment { get; set; }
        public string Excitement { get; set; }

        public int UsersId { get; set; }
        public int HotelsId { get; set; }

        public User Users { get; set; }
        public Hotel Hotels { get; set; }
    }
}
