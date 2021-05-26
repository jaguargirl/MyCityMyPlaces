using System.Collections;
using System.Collections.Generic;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        public bool AddLocation(decimal lon, decimal lat, bool shared, string email);
        public bool AddLocation(decimal lon, decimal lat, string comment, string name, bool shared, string email);
        public bool EditLocation(int locationId, string comment, string name, bool shared);
        public bool RemoveLocation(int locationId);
        public Location GetLocation(decimal lon, decimal lat, string email);
        public Location GetLocationById(int locationId);
    }
}
