using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Tourss { get; set; }
        public string ImageAdress { get; set; }

       
        public ICollection<Hotel> Hotels { get; set; }
        public Tour()
        {
            Hotels = new List<Hotel>();
        }
    }
}
