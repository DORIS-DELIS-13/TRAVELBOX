using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOUPE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Type { get; set; }

        public UsersImage UsersImages { get; set; }
        public UsersRoom UsersRooms { get; set; }
        public Order Orders { get; set; }
        public ICollection<TravelBox> TravelBoxs { get; set; }
        public User()
        {
            TravelBoxs = new List<TravelBox>();
        }
    }
}
