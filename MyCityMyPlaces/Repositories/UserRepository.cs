using System.Collections.Generic;
using System.Linq;
using MyCityMyPlaces.Data;
using MyCityMyPlaces.Interfaces;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email.Trim().ToLower());
        }

        public bool AddRelationship(User sourceUser, User destinationUser)
        {
            if (sourceUser == null || destinationUser == null || sourceUser.FamilyRequestsOut.Contains(destinationUser))
                return false;
            sourceUser.FamilyRequestsOut.Add(destinationUser);
            destinationUser.FamilyRequestsIn.Add(sourceUser);
            _context.SaveChanges();
            return true;
        }

        public bool AddRelationship(string sourceEmail, string destinationEmail)
        {
            var sourceUser = GetByEmail(sourceEmail);
            var destinationUser = GetByEmail(destinationEmail);
            return AddRelationship(sourceUser, destinationUser);
        }
        
        public bool RemoveRelationship(User sourceUser, User destinationUser)
        {
            if (sourceUser == null || destinationUser == null || 
                !sourceUser.FamilyRequestsOut.Contains(destinationUser) ||
                !destinationUser.FamilyRequestsIn.Contains(sourceUser))
                return false;
            sourceUser.FamilyRequestsOut.Remove(destinationUser);
            destinationUser.FamilyRequestsIn.Remove(sourceUser);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveRelationship(string sourceEmail, string destinationEmail)
        {
            var sourceUser = GetByEmail(sourceEmail);
            var destinationUser = GetByEmail(destinationEmail);
            return RemoveRelationship(sourceUser, destinationUser);
        }

        public IEnumerable<User> GetFamily(User user)
        {
            return user?.FamilyRequestsIn.Where(fri => user.FamilyRequestsOut.Contains(fri));
        }
        
        public IEnumerable<User> GetFamily(string email)
        {
            return GetFamily(GetByEmail(email));
        }

        public IEnumerable<Location> GetUserLocations(string email)
        {
            var current = GetByEmail(email);
            return current.Locations;
        }
    }
}
