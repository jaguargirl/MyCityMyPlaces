using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCityMyPlaces.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<User> FamilyRequestsIn { get; set; }
        public virtual ICollection<User> FamilyRequestsOut { get; set; }

    }
}
