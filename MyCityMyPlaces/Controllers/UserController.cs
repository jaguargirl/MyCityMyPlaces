using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCityMyPlaces.Data;
using MyCityMyPlaces.Interfaces;

namespace MyCityMyPlaces.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository,
            ILocationRepository locationRepository,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }
    }
}
