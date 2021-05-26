using System;
using MyCityMyPlaces.Data;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;
using System.Linq;


namespace MyCityMyPlaces.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context): base(context)
        { 
        }

        public Location GetLocation(decimal lon, decimal lat, string email)
        {
            return _context.Locations.FirstOrDefault(l => l.Lon == lon && l.Lat == lat && l.UserEmail == email.Trim().ToLower());
        }

        public Location GetLocationById(int locationId)
        {
            return _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
        }
        
        public bool AddLocation(decimal lon, decimal lat, bool shared, string email)
        {
            User current = _context.Users.FirstOrDefault(u => u.Email == email.Trim().ToLower());
            Location location = new Location(lon, lat, shared, email);
            _context.Locations.Add(location);
            current.Locations.Add(location);
            _context.SaveChanges();
            return true;
        }
        
        public bool AddLocation(decimal lon, decimal lat, string comment, string name, bool shared, string email)
        {
            _context.Locations.Add(new Location(lon, lat, comment, name, shared, email));
            _context.SaveChanges();
            return true;
       }

        public bool EditLocation(int locationId, string comment, string name, bool shared)
        {
            Location location = GetLocationById(locationId);
            location.Comment = comment;
            location.Name = name;
            location.Shared = shared;
            _context.SaveChanges();
            return true;
        }


        public bool RemoveLocation(int locationId)
        {
            Location location = GetLocationById(locationId);
            _context.Locations.Remove(location);
            _context.SaveChanges();
            return true;
        }
    }
}
