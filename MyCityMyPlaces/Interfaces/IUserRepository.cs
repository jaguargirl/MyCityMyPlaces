using System.Collections.Generic;
using MyCityMyPlaces.Models;

namespace MyCityMyPlaces.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByEmail(string email);
        public bool AddRelationship(User sourceUser, User destinationUser);

        public bool AddRelationship(string sourceEmail, string destinationEmail);
        public IEnumerable<User> GetFamily(User user);
        public IEnumerable<User> GetFamily(string email);
        bool RemoveRelationship(User sourceUser, User destinationUser);
        bool RemoveRelationship(string sourceEmail, string destinationEmail);

        public IEnumerable<Location> GetUserLocations(string email);

    }
}
