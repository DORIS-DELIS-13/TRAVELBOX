using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int QuantityAdults { get; set; }
        public int QuantityChildren { get; set; }
        public double TotalCost { get; set; }

        public int HotelsId { get; set; }
        public string UsersId { get; set; }
        public virtual User Users { get; set; }
        public Hotel Hotels { get; set; }
    }
}
