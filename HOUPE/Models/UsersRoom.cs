using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class UsersRoom
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int QuantityOrders { get; set; }

        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
