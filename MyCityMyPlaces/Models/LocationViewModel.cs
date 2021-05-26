using System.ComponentModel.DataAnnotations;

namespace MyCityMyPlaces.Models
{
    public class LocationViewModel
    {
        [Required]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool Shared { get; set; }
    }
}