using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HOUPE.Models
{
    public class User: IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Type { get; set; }
        public string UserStatus { get; set; }

        public virtual UsersImage UsersImages { get; set; }
        public virtual UsersRoom UsersRooms { get; set; }
        public virtual Order Orders { get; set; }
        public virtual ICollection<TravelBox> TravelBoxs { get; set; }
        public User()
        {
            TravelBoxs = new List<TravelBox>();
        }
    }
}
