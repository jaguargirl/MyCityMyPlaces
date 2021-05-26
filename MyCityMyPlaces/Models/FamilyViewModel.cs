using System.ComponentModel.DataAnnotations;

namespace MyCityMyPlaces.Models
{
    public class FamilyViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
