using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class UsersImage
    {
        public int Id { get; set; }
        public string ImageAdress { get; set; }

        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
